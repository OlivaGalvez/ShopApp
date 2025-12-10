using Microsoft.EntityFrameworkCore;
using ShopApp.Services;

namespace ShopApp.DataAccess;

public class ShopOutDbContext : DbContext
{
    public DbSet<CompraItem> Compras { get; set; }
    private readonly IDatabaseRutaService _datbaseRuta;

    public ShopOutDbContext(IDatabaseRutaService databaseRuta)
    {
        _datbaseRuta = databaseRuta;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"Filename={_datbaseRuta.Get("shopdatabase.db")}";
        optionsBuilder.UseSqlite(connectionString);
    }
}

public record CompraItem(int ClientId, int ProductId, int Cantidad, decimal Precio) 
{ 
    public int Id { get; set; }
}
