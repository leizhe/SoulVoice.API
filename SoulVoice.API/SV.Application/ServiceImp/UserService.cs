using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Entity.Command;
using SV.Repository.Core.Command;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserQueryRepository _userQuery;
        private readonly IUserCommandRepository _userCommand;
        public UserService(IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository
            )
        {
            _userQuery = userQueryRepository;
            _userCommand = userCommandRepository;
        }

        public GetResults<UserDto> GetUsers(PageInput input)
        {
            var result = GetDefault<GetResults<UserDto>>();
            var filterExp = BuildExpression(input);
            var query = _userQuery.GetPage(input.Current,input.Size,out var pageCount,filterExp,new { Name = true });
            result.Total = pageCount;
            result.Data = query.Select(user => new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                State = user.State,
                Name = user.Name,
                Password = "*******",
                Roles = user.UserRoles.Select(z => new RoleDto()
                {
                    Id = z.Role.Id,
                    Name = z.Role.Name,
                    Memo = z.Role.Memo
                }).ToList(),
                TotalRole = user.UserRoles.Count
            }).ToList();

            return result;
        }

        //public UpdateResult UpdateUser(UserDto user)
        //{
        //    var result = GetDefault<UpdateResult>();
        //    var existUser = _userQuery.FindSingle(u => u.Id == user.Id);
        //    if (existUser == null)
        //    {
        //        result.Message = "USER_NOT_EXIST";
        //        result.StateCode = 0x00303;
        //        return result;
        //    }
        //    if (IsHasSameName(existUser.Name, existUser.Id))
        //    {
        //        result.Message = "USER_NAME_HAS_EXIST";
        //        result.StateCode = 0x00302;
        //        return result;
        //    }

        //    _userCommand.Update(p => p.Id == user.Id, u => new User()
        //    {
        //        RealName = user.RealName,
        //        Name = user.Name,
        //        State = user.State,
        //        Email = user.Email
        //    });

        //    result.IsSaved = true;
        //    return result;
        //}

        public CreateResult<long> AddUser(UserDto userDto)
        {
            var result = GetDefault<CreateResult<long>>();
            if (IsHasSameName(userDto.Name, userDto.Id))
            {
                result.Message = "USER_NAME_HAS_EXIST";
                result.StateCode = 0x00302;
                return result;
            }
            var user = new User()
            {
                CreationTime = DateTime.Now,
                Password = "",
                Email = userDto.Email,
                State = userDto.State,
                Name = userDto.Name
            };

            _userCommand.Add(user);
            _userCommand.Commit();
            result.Id = user.Id;
            result.IsCreated = true;
            return result;
        }

        //public DeleteResult DeleteUser(int userId)
        //{
        //    var result = GetDefault<DeleteResult>();
        //    //var user = _userQuery.FindSingle(x => x.Id == userId);
        //    //if (user != null)
        //    //{
        //    //    _userCommand.Delete(user);
        //    //    _userCommand.Commit();
        //    //}
        //    _userCommand.Delete(x => x.Id == userId);
        //    result.IsDeleted = true;
        //    return result;
        //}

        //public UpdateResult UpdatePwd(UserDto user)
        //{
        //    var result = GetDefault<UpdateResult>();
        //    var userEntity = _userQuery.FindSingle(x => x.Id == user.Id);
        //    if (userEntity == null)
        //    {
        //        result.Message = string.Format("当前编辑的用户“{0}”已经不存在", user.Name);
        //        return result;
        //    }

        //    _userCommand.Update(p => p.Id == user.Id, u => new User()
        //    {
        //        Password = user.Password
        //    });
        //    result.IsSaved = true;
        //    return result;
        //}

        public GetResult<UserDto> GetUser(long userId)
        {
            var result = GetDefault<GetResult<UserDto>>();
            var model = _userQuery.FindSingle(userId);
            if (model == null)
            {
                result.Message = "USE_NOT_EXIST";
                result.StateCode = 0x00402;
                return result;
            }
            result.Data = new UserDto()
            {
                CreationTime = model.CreationTime,
                Email = model.Email,
                Id = model.Id,
                State = model.State,
                Name = model.Name,
                Password = "*******"
            };
            return result;
        }

        ////public UpdateResult UpdateRoles(UserDto user)
        ////{
        ////    var result = GetDefault<UpdateResult>();
        ////    var model = _userRepository.FindSingle(x => x.Id == user.Id);
        ////    if (model == null)
        ////    {
        ////        result.Message = "USE_NOT_EXIST";
        ////        result.StateCode = 0x00402;
        ////        return result;
        ////    }

        ////    var list = model.UserRoles.ToList();
        ////    if (user.Roles != null)
        ////    {
        ////        foreach (var item in user.Roles)
        ////        {
        ////            if (!list.Exists(x => x.Role.Id == item.Id))
        ////            {
        ////                _userRoleRepository.Add(new UserRole { RoleId = item.Id, UserId = model.Id });
        ////            }
        ////        }

        ////        foreach (var item in list)
        ////        {
        ////            if (!user.Roles.Exists(x => x.Id == item.Id))
        ////            {
        ////                _userRoleRepository.Delete(item);
        ////            }
        ////        }

        ////        _userRoleRepository.Commit();
        ////        _userRepository.Commit();
        ////    }

        ////    result.IsSaved = true;
        ////    return result;
        ////}

        ////public DeleteResult DeleteRole(int userId, int roleId)
        ////{
        ////    var result = GetDefault<DeleteResult>();
        ////    var model = _userRoleRepository.FindSingle(x => x.UserId == userId && x.RoleId == roleId);
        ////    if (model != null)
        ////    {
        ////        _userRoleRepository.Delete(model);
        ////        _userRoleRepository.Commit();
        ////    }

        ////    result.IsDeleted = true;
        ////    return result;
        ////}

        public bool Exist(string username, string password)
        {
            return _userQuery.FindSingle(u => u.Name == username && u.Password == password) != null;
        }

        private bool IsHasSameName(string name, long userId)
        {
            return !string.IsNullOrWhiteSpace(name) && _userQuery.Find(u => u.Name == name).Any();
        }

        private Expression<Func<Entity.Query.User, bool>> BuildExpression(PageInput pageInput)
        {
            Expression<Func<Entity.Query.User, bool>> filterExp = user => true;
            if (string.IsNullOrWhiteSpace(pageInput.Name))
                return filterExp;

            switch (pageInput.Type)
            {
                case 0:
                    filterExp = user => user.Name.Contains(pageInput.Name) || user.Email.Contains(pageInput.Name);
                    break;
                case 1:
                    filterExp = user => user.Name.Contains(pageInput.Name);
                    break;
                case 2:
                    filterExp = user => user.Email.Contains(pageInput.Name);
                    break;
            }

            return filterExp;
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
                Phone = input.Phone,
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