﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;

namespace SV.Application
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        private readonly IMapper _mapper;

        public AutoMapperProfile(IMapper mapper)
        {
            _mapper = mapper;
        }
        public AutoMapperProfile()
        {
            CreateMap<Entity.Query.User, Entity.Command.User>();
	        CreateMap<Entity.Query.Menu, MenuDto>();
	        CreateMap<Entity.Query.Action, ActionDto>();
	        CreateMap<Entity.Query.Classify, ClassifyDto>();
	        CreateMap<Entity.Query.Album, AlbumDto>()
				.ForMember(dest => dest.CreatorUserName, opt => opt.MapFrom(p=>p.CreatorUser.Name));
	        CreateMap<AlbumInput, Entity.Command.Album>(); 
			//CreateMap<Entity.Query.User, UserDto>()
			//    .ConstructUsing(
			//        dto =>
			//        {
			//            var gender = _mapper.Map<IndividualGender>(dto.Gender);
			//            return _individualFactory.CreateIndividual(dto.FirstName, dto.LastName, gender, dto.BirthDate);
			//        });
		}
    }
}
