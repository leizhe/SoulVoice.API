using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;

namespace SV.Application.Profile
{
    public class AlbumProfile : AutoMapper.Profile
	{
	    private readonly IMapper _mapper;

	    public AlbumProfile(IMapper mapper)
	    {
		    _mapper = mapper;
	    }
	    public AlbumProfile()
	    {
			CreateMap<Entity.Album, AlbumDto>()
			    .ForMember(dest => dest.CreatorUserName, opt => opt.MapFrom(p => p.CreatorUser.Name));
		    CreateMap<AlbumInput, Entity.Album>();
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
