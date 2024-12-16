using EnterpriseEntityLayer.DTOs;
using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseBusinessLayer.Abstract
{
    public interface IProductService
    {
        List<ProductDto> GetAll();
        ProductDto GetById(int id);
        void PurchaseProduct(int productId, int quantity, int userId);

        int GetProductDetail(int productId);

    }
}
