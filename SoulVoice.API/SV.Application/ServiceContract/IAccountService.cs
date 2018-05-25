using System;
using System.Collections.Generic;
using System.Text;
using SV.Application.Input;
using SV.Application.Output;

namespace SV.Application.ServiceContract
{
    public interface IAccountService
    {
        CreateResult<long> Register(RegisterInput user);

        GetResult<LoginOutput> Login(string nameOrEmail, string passWord);
    }
}
