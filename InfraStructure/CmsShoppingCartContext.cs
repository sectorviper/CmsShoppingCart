using CmsShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.InfraStructure
{
    public class CmsShoppingCartContext : DbContext
    {
        public CmsShoppingCartContext(DbContextOptions<CmsShoppingCartContext> options)
            : base(options)
        {

        }
        public DbSet<Page> Pages { get; set; }
    }
}
