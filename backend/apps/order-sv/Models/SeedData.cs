
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
                IEnumerable<Product>? products = getProduct();

                IList<Order> orders = new List<Order>();

                foreach(int num in nums)
                {
                    IEnumerable<Product>? calProd = products!.Where(p => p.ProductId%2 == 0);
                    if(calProd.Any())
                    {
                        orders.Add(
                            new Order{
                                OrderId=num,
                                Amount=calProd.Select(p => p.Price).Sum(),
                                Products=calProd.ToList()
                            }
                        );
                    }
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

        public static IEnumerable<Product>? getProduct(){
            string url = "http://localhost:5110/api/products";
            var handler = new HttpClientHandler() 
            { 
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            HttpClient client = new HttpClient(handler);
            HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();
            string content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonSerializer.Deserialize<IEnumerable<Product>>(content);
        }
    }
}