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
            IoCContainer.Register(typeof(DapperRepositoryBase<>).Assembly, "QueryRepository");
            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>).Assembly, "CommandRepository");
            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>), typeof(IEntityFrameworkCommandRepository<>));
            IoCContainer.Register(typeof(DapperRepositoryBase<>), typeof(IDapperQueryRepository<>));
            IoCContainer.Register(typeof(BaseService).Assembly, "Service");
            return IoCContainer.Build(services);
        }
    }
}
