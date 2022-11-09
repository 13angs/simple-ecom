
using SeBackend.Common.Models;
using System.Text.Json;

namespace order_sv.Models
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                OrderContext context = scope.ServiceProvider.GetRequiredService<OrderContext>();

                int[] nums = {1, 2};

                IList<Order> orders = new List<Order>();

                foreach(int num in nums)
                {
                    orders.Add(
                        new Order{
                            OrderId=num,
                            Amount=0,
                            Products=new List<Product>()
                        }
                    );
                }

                try {
                    context.Orders.AddRange(orders);
                    int result = context.SaveChanges();
                    if(result > 0)
                    {
                        Console.WriteLine("--> Successfully Seeding order!");
                    }
                }catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}