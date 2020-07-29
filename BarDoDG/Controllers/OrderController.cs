using BarDoDG.ApiModels;
using BarDoDG.Models;
using BarDoDG.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IEnumerable<IdentifierResponse> Get()
        {
            return _orderRepository.Get();
        }

        [HttpGet("{id}")]
        public OrderResponse Get(int id)
        {
            return _orderRepository.GetOrder(id);
        }

        [HttpPost]
        public IActionResult Post(OrderItemRequest orderItemRequest)
        {
            var errorMessage = _orderRepository.AddItem(orderItemRequest);
            var response = new DefaultResponse
            {
                Success = string.IsNullOrWhiteSpace(errorMessage),
                ErrorMessage = errorMessage
            };

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            var response = new DefaultResponse
            {
                Success = _orderRepository.OrderClose(id)
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new DefaultResponse
            {
                Success = _orderRepository.OrderReset(id)
            };
            return Ok(response);
        }
    }
}

