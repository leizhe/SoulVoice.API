using System;
using System.Collections.Generic;
using System.Text;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Entity.Command;
using SV.Repository.Core.Command;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAccountQueryRepository _accountQuery;
        private readonly IUserQueryRepository _userQuery;
        private readonly IUserCommandRepository _userCommand;
        public AccountService(IAccountQueryRepository accountQueryRepository,
            IUserQueryRepository userQueryRepository, 
            IUserCommandRepository userCommandRepository)
        {
            _accountQuery = accountQueryRepository;
            _userQuery = userQueryRepository;
            _userCommand = userCommandRepository;
        }

        public GetResult<LoginOutput> Login(string nameOrEmail, string passWord)
        {
            var user = _userQuery.GetByUser(p =>
                p.Name.Equals(nameOrEmail) || p.Email.Equals(nameOrEmail) && p.Password.Equals(passWord));
            var permission= _accountQuery.GetPermissions(user.UserRoles)
            throw new NotImplementedException();
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
