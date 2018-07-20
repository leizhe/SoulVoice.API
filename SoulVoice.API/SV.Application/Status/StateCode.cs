using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Application.Status
{
    public enum StatusCode
    {
        Succeed = 0x0000,
        //System
        ModelStateError= 0x1000,
        QueryFailed = 0x1001,
        AddFailed = 0x1002,
        UpdateFailed = 0x1003,
        DeleteFailed = 0x1004,
        ExistedFailed = 0x1005,
        NotExistFailed = 0x1006,
        
        //Account
        NameOrPasswordWrong = 0x2000,
        AccountNotAvalible = 0x2001,
        OldPasswordWrong = 0x2002,
        UserPhoneHasExist = 0x2003,
        UserEmailHasExist = 0x2004,
        UserNameHasExist = 0x2005,
        //1.kkkkkkkk

        //2.hkjhk

        失败 = 0x8000,
        没有执行 = 0x8001,
        未知错误 = 0x8FFF
    }
}
