using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SelfCheckoutMachine.Model
{
    public class CurrencyContext : DbContext
    {
        private IConfiguration Configuration { get; set; }

        public DbSet<Denomination> Denominations { get; set; }

        public CurrencyContext(IConfiguration configuration) : base()
        {
            this.Configuration = configuration;
        }

        public CurrencyContext(IConfiguration configuration, DbContextOptions<CurrencyContext> options) : base(options)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var host = this.Configuration.GetValue<string>("DatabaseConfig:Host");
            var database = this.Configuration.GetValue<string>("DatabaseConfig:Database");
            var port = this.Configuration.GetValue<int>("DatabaseConfig:Port");
            var user = this.Configuration.GetValue<string>("DatabaseConfig:UserName");
            var password = this.Configuration.GetValue<string>("DatabaseConfig:Password");

            var connectionString = $"server={host};database={database};user={user};password={password};port={port}";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)))
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Denomination>()
                .HasData(
                    new Denomination { Value = "5",     Amount = 0 },
                    new Denomination { Value = "10",    Amount = 0 },
                    new Denomination { Value = "20",    Amount = 0 },
                    new Denomination { Value = "50",    Amount = 0 },
                    new Denomination { Value = "100",   Amount = 0 },
                    new Denomination { Value = "200",   Amount = 0 },
                    new Denomination { Value = "500",   Amount = 0 },
                    new Denomination { Value = "1000",  Amount = 0 },
                    new Denomination { Value = "2000",  Amount = 0 },
                    new Denomination { Value = "5000",  Amount = 0 },
                    new Denomination { Value = "10000", Amount = 0 },
                    new Denomination { Value = "20000", Amount = 0 }
                );
        }
    }
}
