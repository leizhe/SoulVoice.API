using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SV.Entity;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
	public class AlbumQueryRepository : DapperRepositoryBase<Album>, IAlbumQueryRepository
	{
		public Album GetById(long albumId)
		{
			var where = $@"WHERE a.Id={albumId}";
			return GetSingleByWhere(where);
		}

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

		private string BaseIncludeSoundSql()
		{
			return @"SELECT a.*,u.*,s.* FROM album AS a 
						LEFT JOIN User AS u ON a.CreatorUserId=u.Id
						LEFT JOIN Sound AS s ON a.Id=s.AlbumId " ;
		}
		private Album GetSingleByWhere(string where)
		{
			var sql = BaseIncludeSoundSql();
			if (!string.IsNullOrEmpty(where))
			{
				sql += where;
			}
			return GetAlbumDictionary(sql).FirstOrDefault();
		}

		private Dictionary<long, Album>.ValueCollection GetAlbumDictionary(string sql)
		{
			var lookup = new Dictionary<long, Album>();
			Conn.Query(sql, FillDicIncludeSound(lookup));
			return lookup.Values;
		}

		private Func<Album, User, Sound, Album> FillDicIncludeSound(Dictionary<long, Album> lookup)
		{
			return (album, user, sound) =>
			{
				if (!lookup.TryGetValue(album.Id, out var tmp))
				{
					tmp = album;
					lookup.Add(album.Id, tmp);
				}
				tmp.Sounds.Add(sound);
				tmp.CreatorUser = user;
				return album;
			};
		}


		private Func<Album, User,Album> FillDic()
		{
			return (album, user) =>
			{
				album.CreatorUser = user;
				return album;
			};
		}


	}
}