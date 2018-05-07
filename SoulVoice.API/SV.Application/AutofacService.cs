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
    class AutofacService
    {
        private IServiceProvider InitIoC(IServiceCollection services)
        {
            var commandString = Configuration.GetConnectionString("CommandDB");
            var queryString = Configuration.GetConnectionString("QueryDB");
            var dbContextOption = new DbContextOption
            {
                CommandString = commandString,
                QueryString = queryString
            };
            IoCContainer.Register(Configuration);//注册配置
            IoCContainer.Register(dbContextOption);//注册数据库配置信息
            IoCContainer.Register(typeof(DapperContext));
            IoCContainer.Register(typeof(EntityFrameworkContext));
            IoCContainer.Register(typeof(DapperRepositoryBase<>).Assembly, "QueryRepository");//注册仓储
            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>).Assembly, "CommandRepository");//注册仓储
            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>), typeof(IEntityFrameworkCommandRepository<>));
            IoCContainer.Register(typeof(DapperRepositoryBase<>), typeof(IDapperQueryRepository<>));
            IoCContainer.Register(typeof(BaseService).Assembly, "Service");
            return IoCContainer.Build(services);
        }
    }
}
