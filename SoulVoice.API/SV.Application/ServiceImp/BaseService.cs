﻿namespace SV.Application.ServiceImp
{
    public class BaseService
    {
        protected T GetDefault<T>() where T : new()
        {
            return new T();
        } 
    }
}