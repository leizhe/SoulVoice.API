using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;

namespace SV.Application.ServiceContract
{
    public interface ISubscriptionService
	{
		GetResults<SubscriptionDto> GetSubscriptionPageBySubscriber(long subscriber, PageInput input);
		CreateResult<long> AddSubscription(SubscriptionInput input);
		DeleteResult DeleteSubscription(SubscriptionInput input);
	}
}