using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SV.Repository.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SV.API
{
    public class Bootstrapper
    {
    }

    //Code First Open
    //public class DbContextFactory : IDesignTimeDbContextFactory<EntityFrameworkContext>
    //{
    //    public EntityFrameworkContext CreateDbContext(string[] args)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json")
    //            .Build();
    //        var builder = new DbContextOptionsBuilder<EntityFrameworkContext>();
    //        var commandString = configuration.GetConnectionString("CommandDB");
    //        builder.UseMySQL(commandString);
    //        return new EntityFrameworkContext(builder.Options);
    //    }
    //}
}
