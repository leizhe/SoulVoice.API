using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.API.Jwt;

namespace SV.API.Controllers
{
	//[Authorize]
	public class SubscriptionController : BaseController
	{
		private readonly ISubscriptionService _subscriptionService;
		public SubscriptionController(ISubscriptionService subscriptionService)
		{
			_subscriptionService = subscriptionService;
		}

		[HttpGet]
		public GetResults<SubscriptionDto> Page(long subscriber, PageInput input)
		{
			return _subscriptionService.GetSubscriptionPageBySubscriber(subscriber, input);
		}

		[HttpPost]
		public CreateResult<long> Add(SubscriptionInput input)
		{
			CheckModelState();
			return _subscriptionService.AddSubscription(input);
		}

		[HttpDelete]
		public DeleteResult Delete(SubscriptionInput input)
		{
			CheckModelState();
			return _subscriptionService.DeleteSubscription(input);
		}
	}

}
