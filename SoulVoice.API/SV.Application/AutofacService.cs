using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SV.Application.ServiceImp;
using SV.Common.IoC;
using SV.Common.Options;
using SV.Repository.Base;
using SV.Repository.Core;

namespace SV.Application
{
    public class AutofacService
    {
        public static IServiceProvider InitIoC(IServiceCollection services)
        {
            IocContainer.Register(typeof(DapperRepositoryBase<>).Assembly, "QueryRepository");
            IocContainer.Register(typeof(EntityFrameworkRepositoryBase<>).Assembly, "CommandRepository");
            IocContainer.Register(typeof(DapperRepositoryBase<>), typeof(IDapperQueryRepository<>));
            IocContainer.Register(typeof(EntityFrameworkRepositoryBase<>), typeof(IEntityFrameworkCommandRepository<>));
            IocContainer.Register(typeof(BaseService).Assembly, "Service");
            return IocContainer.Build(services);
        }
    }
}
