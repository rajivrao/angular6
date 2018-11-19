using Microsoft.EntityFrameworkCore;
using PrototypeApi.DbModels;

namespace PrototypeApi
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}