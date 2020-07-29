using BarDoDG.ApiModels;
using BarDoDG.Data;
using BarDoDG.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {

        }

        public IEnumerable<IdentifierResponse> Get()
        {
            var response = _dbSet.ToList();

            return response.Select(p => new IdentifierResponse
            {
                Id = p.Id,
                Description = p.Code
            });
        }

        public OrderResponse GetOrder(int id)
        {
            var order = _dbSet
                .Include(i => i.Items)
                .ThenInclude(o => o.Product)
                .SingleOrDefault(o => o.Id == id);

            var orderAmount = order.Items.Sum(p => p.Product.Price * p.Amount);

            var discountBeer = DiscountBeer(order);
            var discountBeerCognac = DiscountBeerCognac(order);

            var discountAmount = discountBeer + discountBeerCognac;
            
            orderAmount = orderAmount - discountAmount;

            var orderResponse = new OrderResponse
            {
                Id = order.Id,
                Code = order.Code,
                Closed = order.Closed,
                DiscountAmount = discountAmount,
                OrderAmount = orderAmount,
                Items = order.Items.Select(item => new OrderItemResponse
                {
                    ProductId = item.Product?.Id ?? 0,
                    Description = item.Product?.Description,
                    Price = item.Product.Price * item.Amount,
                    Amount = item.Amount
                })
   
            };

            return orderResponse;
        }

        public string AddItem(OrderItemRequest orderItemRequest)
        {
            try
            {
                var product = _context.Set<Product>()
                    .SingleOrDefault(p => p.Id == orderItemRequest.ProductId);

                var order = _dbSet
                    .Include(i => i.Items)
                        .ThenInclude(p => p.Product)
                    .FirstOrDefault(o => o.Id == orderItemRequest.OrderId);


                if (ValidateJuice(order, product))
                {
                    var orderItem = order.Items.FirstOrDefault(p => p.Product.Id == product.Id);
                    if (orderItem != null)
                    {
                        orderItem.Amount++;
                    }
                    else
                    {
                        orderItem = new OrderItem(product, order);
                        orderItem.Amount = 1;
                        _context.Set<OrderItem>().Add(orderItem);
                    }

                    _context.SaveChanges();

                    return null;
                }

                return "Não é possível incluir mais de 3 sucos por comanda";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public bool OrderClose(int id)
        {
            try
            {
                var order = _dbSet
                    .Include(i => i.Items)
                        .ThenInclude(p => p.Product)
                    .Where(o => o.Id == id).FirstOrDefault();

                order.Closed = true;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public decimal DiscountBeer(Order order)
        {
            var minBeerToDiscount = 5;
            var beerAmount = order.Items.FirstOrDefault(p => p.Product.Description == "Cerveja")?.Amount ?? 0;
            if (beerAmount < minBeerToDiscount)
                return 0;

            var beerPrice = order.Items.FirstOrDefault(p => p.Product.Description == "Cerveja").Product.Price;

            var discountAmount = (beerAmount / minBeerToDiscount) * beerPrice;

            return discountAmount;
        }

        public decimal DiscountBeerCognac(Order order)
        {
            var minBeerToDiscount = 2;
            var minCognacToDiscount = 3;

            var beerAmount = order.Items.FirstOrDefault(p => p.Product.Description == "Cerveja")?.Amount ?? 0;
            var cognacAmount = order.Items.FirstOrDefault(p => p.Product.Description == "Conhaque")?.Amount ?? 0;
            var waterSum = order.Items.Where(p => p.Product.Description == "Água").Sum(x => x.Product.Price);
            if (beerAmount < minBeerToDiscount || cognacAmount < minCognacToDiscount || waterSum <= 0)
                return 0;

            var waterPrice = order.Items.FirstOrDefault(p => p.Product.Description == "Água").Product.Price;

            var cognacDiscountValue = (cognacAmount / minCognacToDiscount) * waterPrice;
            var beerDiscountValue = (beerAmount / minBeerToDiscount) * waterPrice;

            var discountAmount = cognacDiscountValue;
            if (beerDiscountValue < cognacDiscountValue)
                discountAmount = beerDiscountValue;

            discountAmount = discountAmount <= waterSum ? discountAmount : waterSum;
            return discountAmount;
        }


        public bool OrderReset(int id)
        {
            try
            {
                var order = _dbSet.Where(o => o.Id == id)
                    .Include(i => i.Items)
                    .FirstOrDefault();
                order.Closed = false;

                order.Items.RemoveAll(i => i.Order.Id == order.Id);

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Create(List<Order> orders)
        {
            foreach (var item in orders)
            {
                if (!_dbSet.Where(p => p.Code == item.Code).Any())
                {
                    _dbSet.Add(new Order(item.Code, item.Closed));
                }
            }
            _context.SaveChanges();
        }

        public bool ValidateJuice(Order order, Product product)
        {

            if (product.Description != "Suco")
                return true;

            var juiceAmount = order.Items.FirstOrDefault(j => j.Product.Id == product.Id)?.Amount ?? 0;
            var maxJuiceAmount = 3;

            if (juiceAmount >= maxJuiceAmount)
                return false;
            else
                return true;
        }
    }
}

