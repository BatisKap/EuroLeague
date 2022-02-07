using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.Models
{
    public class Euroleague2020_21ASPDBContext : DbContext
    {
        public DbSet<Teams> Team { get; set; }
        public DbSet<Matches> Match { get; set; }
        public Euroleague2020_21ASPDBContext() : base()
        {

        }
        public Euroleague2020_21ASPDBContext(DbContextOptions<Euroleague2020_21ASPDBContext> options) : base(options)
        {

        }
    }
}
