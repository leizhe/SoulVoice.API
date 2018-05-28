using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Application.Status
{
    public enum StatusCode
    {
        Succeed = 0x0000,
        //Account
        NameOrPasswordWrong = 0x1001,
        AccountNotAvalible = 0x1002,
        原密码不正确 = 0x1003,
        电话号已被注册 = 0x1004,
        电子邮件已被注册 = 0x1005,
        用户名已被注册 = 0x1006,
        没有登录 = 0x1007,
        //1.kkkkkkkk

        //2.hkjhk

        失败 = 0x8000,
        没有执行 = 0x8001,
        未知错误 = 0x8FFF
    }
}
