﻿using ED.Application.Dtos;

namespace ED.Application.ServiceContract
{
    public interface IUserService
    {
        GetResults<UserDto> GetUsers(PageInput input);

        UpdateResult UpdateUser(UserDto user);

        CreateResult<long> AddUser(UserDto user);

        DeleteResult DeleteUser(int userId);

        UpdateResult UpdatePwd(UserDto user);

        GetResult<UserDto> GetUser(int userId);

        //UpdateResult UpdateRoles(UserDto user);

        //DeleteResult DeleteRole(int userId, int roleId);

        bool Exist(string username, string password);
    }
}