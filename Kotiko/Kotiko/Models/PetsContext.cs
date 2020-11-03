using Kotiko.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Kotiko.Models
{
    public class PetsContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        //public PetsContext(DbContextOptions<PetsContext> options)
        //    : base(options)
        //{
        //    Database.EnsureCreated();
        //}

        //internal void SaveChanges()
        //{
        //    throw new NotImplementedException();
        //}

        public PetsContext() : base("mobilesdb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PetsContext, Configuration>());
        }
    }
}
