﻿using System.Collections.Generic;
using SV.Entity;

namespace SV.Repository.Core.Query
{
    public interface IAlbumQueryRepository : IDapperQueryRepository<Album>
    {
	    Album GetById(long albumId);
		List<Album> GetPageByClassifyId(int pageNum, int pageSize, out long outTotal, long classifyId);
		List<Album> GetPage(int pageNum, int pageSize, out long outTotal, string where = null, object sortList = null);
	    List<Album> FilterPage(int pageNum, int pageSize, out long outTotal, string filter);
	}
}