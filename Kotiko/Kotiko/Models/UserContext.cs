using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Kotiko.Models
{
    public class UserContext : DbContext
    {
            public DbSet<User> Users { get; set; }
        public UserContext()
            : base("DefaultConnection")
        {
       
        }
    }
}

