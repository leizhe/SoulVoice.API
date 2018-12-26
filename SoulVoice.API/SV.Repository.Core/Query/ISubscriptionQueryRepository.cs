using System.Collections.Generic;
using SV.Entity;

namespace SV.Repository.Core.Query
{
    public interface ISubscriptionQueryRepository : IDapperQueryRepository<Subscription>
    {
	    List<Subscription> GetPage(int pageNum, int pageSize, out long outTotal, string where = null, object sortList = null);
		List<Subscription> GetPageBySubscriber(int pageNum, int pageSize, out long outTotal, long subscriber);
	}
}