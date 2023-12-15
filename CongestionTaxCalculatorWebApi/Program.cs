using Infrastructure.CongestionTaxCalculatorDbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CongestionTaxCalculatorDB>(options =>
{
    options.UseSqlServer(@"Server =.;Database=CongestionTaxCalculatorDB;Trusted_Connection=true;");
}, ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
