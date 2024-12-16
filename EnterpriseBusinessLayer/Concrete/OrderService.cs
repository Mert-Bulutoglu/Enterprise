using AutoMapper;
using EnterpriseBusinessLayer.Abstract;
using EnterpriseDataAccessLayer.Abstract;
using EnterpriseEntityLayer.DTOs;
using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseBusinessLayer.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public void CreateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            if(order == null)
            {
                throw new ArgumentNullException("Order is null");
            }

            _orderRepository.CreateOrder(order);


        }

        public List<OrderDto> GetOrders()
        {
            var orders = _orderRepository.GetAll();

            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
