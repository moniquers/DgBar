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
    public class OrderController: ControllerBase
    {
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public void Get()
        {
            _orderRepository.Get();
        }
    }
}
