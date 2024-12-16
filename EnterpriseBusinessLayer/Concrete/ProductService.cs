using AutoMapper;
using EnterpriseBusinessLayer.Abstract;
using EnterpriseDataAccessLayer.Abstract;
using EnterpriseEntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseBusinessLayer.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;

        public ProductService(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork, IOrderService orderService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _orderService = orderService;
        }

        public List<ProductDto> GetAll()
        {
            var products = _productRepository.GetProducts();

            return _mapper.Map<List<ProductDto>>(products);

        }

        public ProductDto GetById(int id)
        {
            var product = _productRepository.GetProductById(id);

            return _mapper.Map<ProductDto>(product);
        }

        public int GetProductDetail(int productId)
        {
            var product = _productRepository.GetProductById(productId);

            if (product == null) throw new InvalidOperationException("Product not found.");

            return product.Stock;
        }

        public void PurchaseProduct(int productId, int quantity, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var product = _productRepository.GetProductById(productId);

                if (product == null || product.Stock <= 0)
                {
                    throw new InvalidOperationException("Product not found or out of stock.");
                }

                _productRepository.UpdateProductStock(productId, quantity);

                var order = new OrderDto
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UserId = userId,
                    CreatedDate = DateTime.Now
                };

                _orderService.CreateOrder(order);

                _unitOfWork.Commit();


            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }
    }
}
