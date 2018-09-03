using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class MenuQueryRepository : DapperRepositoryBase<Menu>, IMenuQueryRepository
	{


		public List<Menu> GetByIds(List<long> menulst, List<long> actionlst)
		{
			var where= $@"where m.Id IN({ GetIdsString(menulst)}) and a.Id IN({ GetIdsString(actionlst)})";
			return GetListByWhere(where);
		}

		

		public List<Menu> GetAll()
        {
            return GetListByWhere(null);
        }

        private string BaseSql()
        {
            return @"SELECT * FROM menu AS m 
                        LEFT JOIN action AS a ON m.Id=a.MenuId ";
        }

        private List<Menu> GetListByWhere(string where)
        {

            var sql = BaseSql();
            if (!string.IsNullOrEmpty(where))
            {
                sql += where;
            }
            return GetUserDictionary(sql).ToList();
        }
        
        
        private Dictionary<long, Menu>.ValueCollection GetUserDictionary(string sql)
        {
            var lookup = new Dictionary<long, Menu>();
            Conn.Query(sql, FillDic(lookup));
            return lookup.Values;
        }

        private Func<Menu, Entity.Query.Action,Menu> FillDic(Dictionary<long, Menu> lookup)
        {
            return (m, a) =>
            {
	            if (!lookup.TryGetValue(m.Id, out var tmp))
	            {
		            tmp = m;
		            lookup.Add(m.Id, tmp);
	            }

	            tmp.Actions.Add(a);

	            return m;
			};
        }

	
	}
}