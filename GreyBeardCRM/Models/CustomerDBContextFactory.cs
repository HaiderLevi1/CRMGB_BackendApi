using GreyBeardCRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

public class CustomerDBContextFactory : IDesignTimeDbContextFactory<CustomerDBContext>
{
    public CustomerDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CustomerDBContext>();

        var connectionString = ConfigurationManager.ConnectionStrings["CustomerDBCon"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);

        return new CustomerDBContext(optionsBuilder.Options);
    }
}
