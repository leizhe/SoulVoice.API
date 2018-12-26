using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;

namespace SV.Application
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        //private readonly IMapper _mapper;

        //public AutoMapperProfile(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}
        public AutoMapperProfile()
        {
			//CreateMap<Entity.Query.User, Entity.Command.User>();
	        CreateMap<Entity.Menu, MenuDto>();
	        CreateMap<Entity.Action, ActionDto>();
	        CreateMap<Entity.Classify, ClassifyDto>();
	        CreateMap<Entity.Sound, SoundDto>();
	       
			

		}
    }
}
