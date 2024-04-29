using Huddle_CRUD.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle_CRUD.Persistance
{
    public class HuddleDbContext : DbContext, IHuddleDbContext
    {
        public HuddleDbContext(DbContextOptions<HuddleDbContext> options) : base(options) { }
        public DbSet<TeamModel> Teams { get; set; }
    }
}
