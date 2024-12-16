using EnterpriseEntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseBusinessLayer.Abstract
{
    public interface IOrderService
    {
        void CreateOrder(OrderDto order);

        List<OrderDto> GetOrders(); 
    }
}
