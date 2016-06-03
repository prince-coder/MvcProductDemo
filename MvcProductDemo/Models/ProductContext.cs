using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MvcProductDemo.Models
{
    public class ProductContext: DbContext
    {
        public ProductContext(): base("ProductContext")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        {
            modelbuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}