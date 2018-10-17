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
