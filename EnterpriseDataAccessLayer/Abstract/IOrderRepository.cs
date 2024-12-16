using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Abstract
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        void CreateOrder(Order order);
        List<Order> GetAll();
    }
}
