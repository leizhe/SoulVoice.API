using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SV.Entity;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class SubscriptionQueryRepository : DapperRepositoryBase<Subscription>, ISubscriptionQueryRepository
	{
		public List<Subscription> GetPage(int pageNum, int pageSize, out long outTotal, string @where = null, object sortList = null)
		{
			var baseSql = BaseIncludeSubscriptionSql();
			var pageSql = GetPageSql(baseSql, "subscription", "s", pageNum, pageSize) + where;
			var lookup = new Dictionary<long, Subscription>();
			using (var multi = Conn.QueryMultiple(pageSql))
			{
				outTotal = multi.Read<int>().Single();
				multi.Read(FillDicIncludeSound(lookup), splitOn: "Id");
				return lookup.Values.ToList();
			}
		}

		public List<Subscription> GetPageBySubscriber(int pageNum, int pageSize, out long outTotal, long subscriber)
		{
			var where = $"WHERE s.Subscriber={subscriber} ORDER BY s.SubscriptionDate DESC";
			return GetPage(pageNum, pageSize, out outTotal, where);
		}

		private string BaseIncludeSubscriptionSql()
		{
			return @"SELECT s.*,a.*,u.* FROM subscription AS s 
						LEFT JOIN album AS a ON s.AlbumId=a.Id 
						LEFT JOIN User AS u ON a.CreatorUserId=u.Id ";
		}

		private Func<Subscription, Album, User, Subscription> FillDicIncludeSound(Dictionary<long, Subscription> lookup)
		{
			
			return (subscription, album, user) =>
			{
				if (!lookup.TryGetValue(subscription.Id, out var tmp))
				{
					tmp = subscription;
					lookup.Add(subscription.Id, tmp);
				}
				tmp.Album=album;
				if (tmp.Album!=null)
				{
					tmp.Album.CreatorUser = user;
				}
				return subscription;
			};
		}


	}
}