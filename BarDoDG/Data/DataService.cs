using BarDoDG.Models;
using BarDoDG.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Data
{
    public class DataService : IDataService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ApplicationContext _context;

        public DataService(ApplicationContext context, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public void StartDb()
        {
            _context.Database.Migrate();

            List<Product> products = new List<Product>{
                   new Product{Description = "Cerveja", Price = 5},
                   new Product{Description = "Conhaque", Price = 20},
                   new Product{Description = "Suco", Price = 50},
                   new Product{Description = "Água", Price = 70}
                   };

            List<Order> orders = new List<Order>
            {
                new Order{Code = "CMD123", Closed = false},
                new Order{Code = "CMD124", Closed = false},
                new Order{Code = "CMD125", Closed = false},
                new Order{Code = "CMD126", Closed = false}
            };

            _productRepository.Create(products);
            _orderRepository.Create(orders);
        }
    }
}
