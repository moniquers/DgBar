using System.Collections.Generic;
using BarDoDG.ApiModels;
using BarDoDG.Models;

namespace BarDoDG.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<IdentifierResponse> Get();
        void Create(List<Product> products);
    }
}