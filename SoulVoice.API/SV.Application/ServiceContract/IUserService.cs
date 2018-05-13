using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;

namespace SV.Application.ServiceContract
{
    public interface IUserService
    {
        CreateResult<long> Register(RegisterInput user);


        GetResults<UserDto> GetUsers(PageInput input);

        //UpdateResult UpdateUser(UserDto user);


        //CreateResult<long> AddUser(UserDto user);

        //DeleteResult DeleteUser(int userId);

        //UpdateResult UpdatePwd(UserDto user);

        GetResult<UserDto> GetUser(long userId);

        ////UpdateResult UpdateRoles(UserDto user);

        ////DeleteResult DeleteRole(int userId, int roleId);

        //bool Exist(string username, string password);
    }
}