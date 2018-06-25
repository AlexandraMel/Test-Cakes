using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CakeExchange.Models
{
    public class CakeContext: DbContext
    {
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public  DbSet<History> History { get; set; }

        public CakeContext(DbContextOptions<CakeContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cakesdb;Username=postgres;Password=postgres");
        }


    }
}
