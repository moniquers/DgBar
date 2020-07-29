using BarDoDG.ApiModels;
using BarDoDG.Data;
using BarDoDG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<IdentifierResponse> Get()
        {
            var response = _dbSet.ToList();

            return response.Select(p => new IdentifierResponse
                {
                    Id = p.Id,
                    Description = p.Description
                }
            );
        }

        public void Create(List<Product> products)
        {
            foreach (var item in products)
            {
                if (!_dbSet.Where(p => p.Description == item.Description).Any())
                {
                    _dbSet.Add(new Product(item.Description, item.Price));
                }
            }
            _context.SaveChanges();
        }
    }
}
