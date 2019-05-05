using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Entitys : DbContext
    {

        public Entitys()
         : base("name=Entitys")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         //   throw new UnintentionalCodeFirstException();
        }

        public DbSet<Drop> Drops { get; set; }
        public DbSet<Report> Reports { get; set; }
        //to add column after the DB have been created (in code first i guess):
        //Enable-Migrations
        //then:
        //add the field you want to the class in your BE .in this project i added ImagePath(string type) to the Drop class.
        //Add-Migration anyname
        //then:
        //Update-Database
        //thats it!!
    }
}
