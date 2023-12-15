using Domain.Contract.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CongestionTaxCalculatorDbContext
{
    public class CongestionTaxCalculatorDB : DbContext
    {
        public CongestionTaxCalculatorDB(DbContextOptions<CongestionTaxCalculatorDB> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeeTaxRule>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.Id).ValueGeneratedOnAdd();
                b.Property(o => o.FromDateTime);
                b.Property(o => o.ToDateTime);
                b.Property(o => o.Amount);
            });

            modelBuilder.Entity<MaxfeeTax>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.Id).ValueGeneratedOnAdd();
                b.Property(o => o.IsActive);
                b.Property(o => o.MaxAmount);
            });
        }

        public DbSet<FeeTaxRule> FeeTaxRules { get; set; }
        public DbSet<MaxfeeTax> MaxfeeTax { get; set; }
    }
}