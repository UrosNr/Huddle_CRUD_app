using Huddle_CRUD.Core.Common;
using Huddle_CRUD.Core.Filters;
using Huddle_CRUD.Core.Models;
using Huddle_CRUD.Core.ViewModels;
using Huddle_CRUD.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Huddle_CRUD_Api.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IHuddleInMemoryDbContext _inMemoryContext;

        public TeamRepository(IHuddleInMemoryDbContext inMemoryContext)
        {
            _inMemoryContext = inMemoryContext;
        }

        public async Task<TeamModel> GetTeamById(Guid id, CancellationToken cancellationToken)
        {
            return await _inMemoryContext.Teams.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TeamModel>> GetAllTeams(CancellationToken cancellationToken)
        {
            return await _inMemoryContext.Teams.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<PaginatedResponse<List<TeamModel>>> FindTeam(TeamFilter filter, CancellationToken cancellationToken)
        {
            var query = _inMemoryContext.Teams.AsNoTracking().AsQueryable();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                if (filter.SortBy.Equals("asc"))
                    query = query.OrderBy(x => x.Name);
                else
                    query = query.OrderByDescending(x => x.Name);
            }
            var count = await query.CountAsync(cancellationToken);

            var entities = await query
            .Skip((filter.Page.Value - 1) * filter.PageSize.Value)
            .Take(filter.PageSize.Value)
            .ToListAsync(cancellationToken);

            var response = new PaginatedResponse<List<TeamModel>>()
            {
                CurrentPage = filter.Page.Value,
                CurrentPageSize = filter.PageSize.Value,
                TotalResultCount = count,
                Result = entities
            };

            return response;
        }

        public async Task<bool> InsertTeam(TeamVm team, CancellationToken cancellationToken)
        {
            var newTeam = new TeamModel();
            newTeam.Name = team.Name;
            newTeam.Timestamp = DateTime.Now;

            _inMemoryContext.Teams.Add(newTeam);

            int result = await _inMemoryContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateTeam(TeamVm team, CancellationToken cancellationToken)
        {
            var existingTeam = await _inMemoryContext.Teams.FirstOrDefaultAsync(t => t.Id == team.Id);
            if (existingTeam != null)
            {
                existingTeam.Name = team.Name;
                existingTeam.Timestamp = DateTime.Now;
                int result = await _inMemoryContext.SaveChangesAsync(cancellationToken);
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteTeam(Guid id, CancellationToken cancellationToken)
        {
            var teamToDelete = await _inMemoryContext.Teams.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (teamToDelete != null)
            {
                _inMemoryContext.Teams.Remove(teamToDelete);
                int result = await _inMemoryContext.SaveChangesAsync(cancellationToken);
                return result > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
