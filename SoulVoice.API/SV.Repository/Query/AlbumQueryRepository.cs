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
    }
}