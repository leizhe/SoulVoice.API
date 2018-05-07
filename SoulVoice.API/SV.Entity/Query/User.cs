using System;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity.Query
{
    [Serializable]
    public class User : BaseEntity, IHasCreationTime, IEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public string Password { get; set; }
        
        public string IdCard { get; set; }

        public decimal Money { get; set; }
        
        public string Alipay { get; set; }
        
        public string WeChat { get; set; }
        
        public string ApplePay { get; set; }

        public DateTime CreationTime { get; set; }

        public int State { get; set; }


        public Role Role { get; set; }
        
    }
    [Serializable]
    public sealed class UserOrmMapper : ClassMapper<User>
    {
        public UserOrmMapper()
        {
            base.Table("User");
            Map(f => f.Role).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}