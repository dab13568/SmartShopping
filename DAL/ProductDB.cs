using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DAL
{
    public class ProductDB : DbContext
    {
        public ProductDB() : base()
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<ScannedProduct> scans { get; set; }


    }
}
