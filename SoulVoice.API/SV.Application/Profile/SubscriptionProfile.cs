using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;

namespace SV.Application.Profile
{
    public class SubscriptionProfile : AutoMapper.Profile
	{
	    private readonly IMapper _mapper;

	    public SubscriptionProfile(IMapper mapper)
	    {
		    _mapper = mapper;
	    }
	    public SubscriptionProfile()
	    {
		    CreateMap<SubscriptionInput, Entity.Command.Subscription>();
		    CreateMap<Entity.Query.Subscription, SubscriptionDto>()
			    //.ForMember(sub => sub.AlbumName, opt => opt.MapFrom(p => p.Album.Name))
			    //.ForMember(sub => sub.AlbumPic, opt => opt.MapFrom(p => p.Album.Pic))
			    //.ForMember(sub => sub.AlbumMemo, opt => opt.MapFrom(p => p.Album.Memo))
			    //.ForMember(sub => sub.AlbumLastUpdate, opt => opt.MapFrom(p => p.Album.LastUpdate))
			    .ForMember(sub => sub.CreatorUserName, opt => opt.MapFrom(p => p.Album.CreatorUser.Name)); 
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
