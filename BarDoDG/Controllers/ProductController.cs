using BarDoDG.ApiModels;
using BarDoDG.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BarDoDG.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<IdentifierResponse> Get()
        {
            return _productRepository.Get();
        }
    }
}
