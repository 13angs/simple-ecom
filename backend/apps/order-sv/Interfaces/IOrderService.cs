using Microsoft.AspNetCore.Mvc;
using SeBackend.Common.Models;

namespace order_sv.Interfaces
{
    public interface IOrderService
    {
        ActionResult<IEnumerable<Order>> Get();
        Task<ActionResult<Order>> Post(Order order);
    }
}