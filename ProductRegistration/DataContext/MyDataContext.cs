using Microsoft.EntityFrameworkCore;
using ProductRegistration.Domain.Models;

namespace ProductRegistration.API.DataContext
{
    public class MyDataContext : DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options): base(options) { }  

        public DbSet<Product> Products { get; set; }
    }
}
