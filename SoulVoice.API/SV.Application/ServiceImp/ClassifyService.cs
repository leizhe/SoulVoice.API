using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class ClassifyService : BaseService, IClassifyService
	{
        private readonly IMapper _mapper;
		private readonly IClassifyQueryRepository _classifyQuery;
		

		public ClassifyService(IMapper mapper,
			IClassifyQueryRepository classifyQueryRepository
		   )
        {
            _mapper = mapper;
	        _classifyQuery = classifyQueryRepository;

        }

		public GetResults<ClassifyDto> GetAll()
		{
			var result = GetDefault<GetResults<ClassifyDto>>();
			var classifies = _classifyQuery.FindAll();
			result.Data= _mapper.Map<List<ClassifyDto>>(classifies);
			result.Total = classifies.Count();
			return result;
		}
	}
}
