using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OnePiece.Infrastructure.Data;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<OpDbContext>
{
    public OpDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OpDbContext>();
        optionsBuilder.UseSqlServer("Server=database-1.cvm0uo8sy0wn.ap-southeast-1.rds.amazonaws.com,1433;Database=onepiece;User Id=admin;Password=Test1234;TrustServerCertificate=True;");

        return new OpDbContext(optionsBuilder.Options);
    }
}

