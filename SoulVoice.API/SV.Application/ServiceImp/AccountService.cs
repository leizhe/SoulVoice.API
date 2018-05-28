using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly IAccountQueryRepository _accountQuery;
        private readonly IUserQueryRepository _userQuery;
        private readonly IUserCommandRepository _userCommand;

        public AccountService(IMapper mapper,
            IAccountQueryRepository accountQueryRepository,
            IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository)
        {
            _mapper = mapper;
            _accountQuery = accountQueryRepository;
            _userQuery = userQueryRepository;
            _userCommand = userCommandRepository;
        }

        public GetResult<LoginOutput> Login(string nameOrEmail, string passWord)
        {
            var result = GetDefault<GetResult<LoginOutput>>();
            var user = _userQuery.GetByAccountAndPassword(nameOrEmail, passWord);
            if (user == null)
            {
                result.Message = "无此用户，请检查账户和密码";
                result.StateCode = (int) StatusCode.NameOrPasswordWrong;
                return result;
            }
            var userMenus = _accountQuery.GetPermissions(user.UserRoles.Select(z => z.RoleId).ToList());

            result.Data = new LoginOutput
            {
                UserName = user.Name,
                Menus = _mapper.Map<List<MenuDto>>(userMenus)
            };
            return result;
        }

        public CreateResult<long> Register(RegisterInput input)
        {
            var result = GetDefault<CreateResult<long>>();
            //if (IsHasSameName(user.Name, user.Id))
            //{
            //    result.Message = "USER_NAME_HAS_EXIST";
            //    result.StateCode = 0x00302;
            //    return result;
            //}
            var user = new User()
            {
                CreationTime = DateTime.UtcNow,
                Password = input.Password,
                Email = input.Email,
                Name = input.Name,
                Phone = input.Phone
                //State = input.State,
            };

            _userCommand.Add(user);
            _userCommand.Commit();
            result.Id = user.Id;
            result.IsCreated = true;
            return result;
        }
    }
}
