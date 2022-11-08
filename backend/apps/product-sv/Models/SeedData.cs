using SeBackend.Common.Models;

namespace product_sv.Models
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app){
            using(var scope = app.ApplicationServices.CreateScope())
            {
                ProductContext context = scope.ServiceProvider.GetRequiredService<ProductContext>();
                string[] productNames = {"Orange", "Apple", "Papaya", "Durian"};
                int[] prices = {100, 200, 50, 500};
                IList<Product> products = new List<Product>();
                for(int i=0; i < productNames.Length; i++)
                {
                    products.Add(
                        new Product{
                            ProductId=i+1,
                            Name=productNames[i],
                            Price=prices[i]
                        }
                    );
                }

                try{

                    context.Products.AddRange(products);
                    int result = context.SaveChanges();

                    if( result > 0)
                    {
                        Console.WriteLine("Successfully seeding Product!");
                    }
                } catch{
                    Console.WriteLine("Failed seeding Product!");
                }
            }
        }
    }
}