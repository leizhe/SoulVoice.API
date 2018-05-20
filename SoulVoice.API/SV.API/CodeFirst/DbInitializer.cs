using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SV.API.CodeFirst;
using SV.Entity.Command;
using SV.Repository.Base;

namespace SV.Application
{
    public class DbInitializer
    {
        public void InitDB(IServiceProvider service)
        {
            var db = service.GetService<EntityFrameworkContext>();
//            if (db != null && db.Database.EnsureCreated())
//            {
//
//
//                Article article = new Article
//                {
//                    Title = "test",
//                    Description = "SMBlog Test"
//                };
//
//                db.Articles.Add(article);
//                await db.SaveChangesAsync();
//            }
        }

        public void DataInit()
        {
            


            //if (DB.Queryable<User>().Any())
            //{
            //    return;   // DB has been seeded
            //}
            //var users = new[]
            //{
            //    new User{Name="Carson",RealName="Alexander Carson",Password = "aaa",CreationTime=DateTime.Parse("2005-09-01")},
            //    new User{Name="Meredith",RealName="Alonso Meredith",Password = "aaa",CreationTime=DateTime.Parse("2002-09-01")},
            //    new User{Name="Arturo",RealName="Anand Arturo",Password = "aaa",CreationTime=DateTime.Parse("2003-09-01")}
            //};
            //DB.Insertable(users).ExecuteCommand();

            //var roles = new[]
            //{
            //    new Role {RoleName = "Admin", CreationTime =DateTime.Parse("2005-09-01") }
            //};
            //DB.Insertable(roles).ExecuteCommand();

            //var userRoles = new[]
            //{
            //    new UserRole {UserId =1,RoleId = 1}
            //};
            //DB.Insertable(userRoles).ExecuteCommand();

        }
    }
}
