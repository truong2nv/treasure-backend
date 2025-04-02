using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnePiece.Core.Entities;

namespace OnePiece.Infrastructure.Data;

public class OpDbContext : DbContext
{
    public OpDbContext(DbContextOptions<OpDbContext> options) 
        : base(options) {}
    public DbSet<TreasureHunt> TreasureHunts { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}