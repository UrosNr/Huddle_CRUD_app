using Huddle_CRUD.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle_CRUD.Persistance
{
    public interface IHuddleDbContext
    {
        DbSet<TeamModel> Teams { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
