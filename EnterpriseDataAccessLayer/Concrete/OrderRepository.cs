using EnterpriseDataAccessLayer.Abstract;
using EnterpriseDataAccessLayer.AppDbContext;
using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(EnterpriseContext ctx) : base(ctx)
        {
        }

        public void CreateOrder(Order order)
        {
            _ctx.Orders.Add(order);
            _ctx.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _ctx.Orders.ToList();
        }
    }
}
