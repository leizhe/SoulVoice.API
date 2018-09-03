using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Application.Status;
using SV.Entity.Command;
using SV.Repository.Core.Command;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class AlbumService : BaseService, IAlbumService
	{
        private readonly IMapper _mapper;
		private readonly IAlbumQueryRepository _albumQuery;
		

		public AlbumService(IMapper mapper,
			IAlbumQueryRepository albumQueryRepository
		   )
        {
            _mapper = mapper;
	        _albumQuery = albumQueryRepository;

        }

	}
}
