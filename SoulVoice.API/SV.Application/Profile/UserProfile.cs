using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SV.Application.Dtos;

namespace SV.Application.MapProfile
{
    public class UserProfile : Profile
    {
        private readonly IMapper _mapper;

        public UserProfile(IMapper mapper)
        {
            _mapper = mapper;
        }
        public UserProfile()
        {
            CreateMap<Entity.Query.User, Entity.Command.User>();
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
