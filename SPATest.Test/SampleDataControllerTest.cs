using PrototypeApi;
using PrototypeApi.DbModels;
using SPATest.Controllers;
using System;
using System.Threading.Tasks;
using TestSupport.EfHelpers;
using Xunit;

namespace SPATest.Test
{
    public class SampleDataControllerTest
    {
        [Fact]
        public void TestGetMethod()
        {            
            var options = EfInMemory.CreateOptions<ApiContext>();
            using (var context = new ApiContext(options))
            {
                // Add a single test product
                var p1 = new Product() { Name = "Nike", Price = 99.99M };
                context.Products.Add(p1);
                context.SaveChanges();

                // Call Get method of controller
                SampleDataController _controller = new SampleDataController(context);
                var products = _controller.Products();

                // Assert the values returned from Get
                foreach (var product in products)
                {
                    Assert.Equal(1, product.ID);
                    Assert.Equal("Nike", product.Name);
                    Assert.Equal(99.99M, product.Price);
                }
            }
        }

        [Fact]
        public async Task TestPostMethod()
        {
            var options = EfInMemory.CreateOptions<ApiContext>();
            using (var context = new ApiContext(options))
            {
                // Create sample data for Post
                Product product = new Product() { Name = "Puma", Price = 100M };
                
                // Call Post method of controller
                SampleDataController _controller = new SampleDataController(context);
                await _controller.Create(product);

                // Assert the values inserted in database
                foreach (var dbProd in context.Products)
                {
                    Assert.Equal(1, dbProd.ID);
                    Assert.Equal("Puma", dbProd.Name);
                    Assert.Equal(100M, dbProd.Price);
                }
            }
        }
    }
}
