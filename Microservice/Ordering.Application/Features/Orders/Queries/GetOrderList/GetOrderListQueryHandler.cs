﻿using AutoMapper;
using MediatR;
using Ordering.Application.Constracts.Persistence;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler: IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request,
            CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orderList);
        }
    }
}