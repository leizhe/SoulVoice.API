using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Dapper;
using SV.Common.Extensions;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
	public class AlbumQueryRepository : DapperRepositoryBase<Album>, IAlbumQueryRepository
	{
		public List<Album> GetPageByClassifyId(int pageNum, int pageSize, out long outTotal, long classifyId)
		{
			var where = $"WHERE a.ClassifyId={classifyId} ORDER BY a.LastUpdate DESC";
			return GetPage(pageNum, pageSize, out outTotal, where);
		}

		public List<Album> FilterPage(int pageNum, int pageSize, out long outTotal, string filter)
		{
			var where = $"WHERE a.`Name` LIKE '%{filter}%' OR u.`Name` LIKE '%{filter}%'";
			return GetPage(pageNum, pageSize, out outTotal, where);
		}

		public List<Album> GetPage(int pageNum, int pageSize, out long outTotal, string where = null, object sortList = null)
		{
			var baseSql = BaseSql();
			var commandSql = GetPageSql(baseSql, "album", "a", pageNum, pageSize) + where;
			using (var multi = Conn.QueryMultiple(commandSql))
			{
				outTotal = multi.Read<int>().Single();
				var result =multi.Read(FillDic(), splitOn: "Id");
				return result.ToList();
			}
		}
		
		private string BaseSql()
		{
			return @"SELECT a.*,u.* FROM album AS a 
						LEFT JOIN User AS u ON a.CreatorUserId=u.Id ";
		}

		private Func<Album, User, Album> FillDic()
		{
			return (album, user) =>
			{
				album.CreatorUser = user;
				return album;
			};
		}


	}
}