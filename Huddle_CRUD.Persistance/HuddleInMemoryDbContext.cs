using Huddle_CRUD.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle_CRUD.Persistance
{
    public class HuddleInMemoryDbContext : DbContext, IHuddleInMemoryDbContext
    {
        public HuddleInMemoryDbContext(DbContextOptions<HuddleInMemoryDbContext> options) : base(options) { }
        public DbSet<TeamModel> Teams { get; set; }
    }
}
