using System;
using System.Collections.Generic;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Application.Status;
using SV.Repository.Core.Command;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class SubscriptionService : BaseService, ISubscriptionService
	{
        private readonly IMapper _mapper;
		private readonly ISubscriptionQueryRepository _subscriptionQuery;
		private readonly ISubscriptionCommandRepository _subscriptionCommand;


		public SubscriptionService(IMapper mapper,
			ISubscriptionQueryRepository subscriptionQueryRepository,
			ISubscriptionCommandRepository subscriptionCommandRepository)
        {
            _mapper = mapper;
	        _subscriptionQuery = subscriptionQueryRepository;
	        _subscriptionCommand = subscriptionCommandRepository;
        }

		public CreateResult<long> AddSubscription(SubscriptionInput input)
		{
			var result = GetDefault<CreateResult<long>>();
			if (IsExists(input))
			{
				result.Message = "The subscription has exist";
				result.StateCode = (int)StatusCode.SubscriptionExist;
				return result;
			}
			var subscription = _mapper.Map<Entity.Command.Subscription>(input);
			subscription.SubscriptionDate=DateTime.UtcNow;
			_subscriptionCommand.Add(subscription);
			_subscriptionCommand.Commit();
			result.Id = subscription.Id;
			result.IsCreated = true;
			return result;
		}

		public DeleteResult DeleteSubscription(SubscriptionInput input)
		{
			var result = GetDefault<DeleteResult>();
			_subscriptionCommand.Delete(f => f.AlbumId == input.AlbumId&&f.Subscriber==input.Subscriber);
			result.IsDeleted = true;
			return result;
		}

		public GetResults<SubscriptionDto> GetSubscriptionPageBySubscriber(long subscriber, PageInput input)
		{
			var result = GetDefault<GetResults<SubscriptionDto>>();
			var subscriptions = _subscriptionQuery.GetPageBySubscriber(input.Current, input.Size, out var pageCount, subscriber);
			result.Data= _mapper.Map<List<SubscriptionDto>>(subscriptions);
			result.Total = pageCount;
			return result;
		}

		private bool IsExists(SubscriptionInput input)
		{
			return _subscriptionQuery.Exists(a => a.AlbumId == input.AlbumId && a.Subscriber == input.Subscriber);
		}

	}
}
