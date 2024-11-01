using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreyBeardCRM.Models
{
    public class CustomerDBContext: DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options):base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}