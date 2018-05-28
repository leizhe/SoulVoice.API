//using AutoMapper;

//namespace SV.Application.Profile
//{
//    public class UserProfile : AutoMapper.Profile
//    {
//        private readonly IMapper _mapper;

//        public UserProfile(IMapper mapper)
//        {
//            _mapper = mapper;
//        }
//        public UserProfile()
//        {
//            CreateMap<Entity.Query.User, Entity.Command.User>();
//            //CreateMap<Entity.Query.User, UserDto>()
//            //    .ConstructUsing(
//            //        dto =>
//            //        {
//            //            var gender = _mapper.Map<IndividualGender>(dto.Gender);
//            //            return _individualFactory.CreateIndividual(dto.FirstName, dto.LastName, gender, dto.BirthDate);
//            //        });
//        }
//    }
//}
