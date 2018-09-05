using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Application.Status;
using SV.Entity.Command;
using SV.Repository.Core.Command;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IPermissionQueryRepository _permissionQuery;
	    private readonly IMenuQueryRepository _menuQuery;
		private readonly IUserQueryRepository _userQuery;
        private readonly IUserCommandRepository _userCommand;
	    private readonly IUserRoleCommandRepository _userRoleCommand;
		

		public AccountService(IMapper mapper,
            IPermissionQueryRepository permissionQueryRepository,
			IMenuQueryRepository menuQueryRepository,
			IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository,
			IUserRoleCommandRepository userRoleCommandRepository)
        {
            _mapper = mapper;
	        _permissionQuery = permissionQueryRepository;
			_menuQuery = menuQueryRepository;
			_userQuery = userQueryRepository;
            _userCommand = userCommandRepository;
	        _userRoleCommand = userRoleCommandRepository;
        }

        public GetResult<LoginOutput> Login(string nameOrEmail, string passWord)
        {
            var result = GetDefault<GetResult<LoginOutput>>();
            var user = _userQuery.GetByAccountAndPassword(nameOrEmail, passWord);
            if (user == null)
            {
                result.StateCode = (int) StatusCode.NameOrPasswordWrong;
                return result;
            }

            result.Data = new LoginOutput
            {
                UserName = user.Name,
                Menus = GetPermissionsByRoleIds(user.UserRoles.Select(z => z.RoleId).ToList())
            };
            return result;
        }

        public CreateResult<long> Register(RegisterInput input)
        {
            var result = GetDefault<CreateResult<long>>();
            if (IsHasSameName(input.Name))
            {
                result.Message = "The name has exist";
                result.StateCode = (int)StatusCode.UserNameHasExist;
                return result;
            }
            if (IsHasSameEmail(input.Email))
            {
                result.Message = "The email has exist";
                result.StateCode = (int)StatusCode.UserEmailHasExist;
                return result;
            }
            if (IsHasSamePhone(input.Phone))
            {
                result.Message = "The phone has exist";
                result.StateCode = (int)StatusCode.UserPhoneHasExist;
                return result;
            }
            
            var user = new User
            {
                CreationTime = DateTime.UtcNow,
                Password = input.Password,
                Email = input.Email,
                Name = input.Name,
                Phone = input.Phone,
                State = AccountStatus.Available
            };
            _userCommand.Add(user);
            _userCommand.Commit();

			var userRole=new UserRole
			{
				UserId = user.Id,
				RoleId = RoleType.Member
			};
	        _userRoleCommand.Add(userRole);
			_userRoleCommand.Commit();

			result.Id = user.Id;
            result.IsCreated = true;
            return result;
        }

	    private List<MenuDto> GetPermissionsByRoleIds(List<long> roleIds)
	    {
		    var permissions= _permissionQuery.GetPermissionsByRoleIds(roleIds);
		    var permissionMenuIds = permissions.Where(p => p.Access.Equals(PermissionAccess.Menu)).Select(p => p.AccessValue).ToList();
		    var permissionActionIds = permissions.Where(p => p.Access.Equals(PermissionAccess.Action)).Select(p => p.AccessValue).ToList();
			var userMenus = _menuQuery.GetByIds(permissionMenuIds, permissionActionIds);
		    return _mapper.Map<List<MenuDto>>(userMenus);

	    }

		private bool IsHasSameName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && _userQuery.Find(u => u.Name == name).Any();
        }

        private bool IsHasSameEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && _userQuery.Find(u => u.Email == email).Any();
        }

        private bool IsHasSamePhone(string phone)
        {
            return !string.IsNullOrWhiteSpace(phone) && _userQuery.Find(u => u.Phone == phone).Any();
        }
    }
}
