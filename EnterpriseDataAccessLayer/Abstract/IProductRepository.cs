using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        void UpdateProductStock(int productId, int quantity);
    }
}
