using Microsoft.AspNetCore.Mvc;
using SeBackend.Common.Models;

namespace product_sv.Interfaces
{
    public interface IProductService
    {
        ActionResult<IEnumerable<Product>> Get();
        Task<ActionResult<Product>> Post(Product product);
    }
}