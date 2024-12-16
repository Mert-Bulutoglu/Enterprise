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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EnterpriseContext ctx) : base(ctx)
        {
        }

        public Product GetProductById(int id)
        {
           return _ctx.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _ctx.Products.ToList();
        }

        public void UpdateProductStock(int productId, int quantity)
        {
            var product = _ctx.Products.FirstOrDefault(p => p.Id == productId);
            product.Stock -= quantity;
            _ctx.SaveChanges();
        }
    }
}
