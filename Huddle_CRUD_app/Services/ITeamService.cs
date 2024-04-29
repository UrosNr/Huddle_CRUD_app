using Huddle_CRUD.Core.Common;
using Huddle_CRUD.Core.Filters;
using Huddle_CRUD.Core.ViewModels;

namespace Huddle_CRUD_app.Services
{
    public interface ITeamService
    {
        Task<TeamVm> GetTeamById(Guid id);
        Task<IEnumerable<TeamVm>> GetAllTeams();
        Task<PaginatedResponse<List<TeamVm>>> FindTeams(TeamFilter filter);
        Task<bool> InsertTeam(TeamVm team);
        Task<bool> UpdateTeam(TeamVm team);
        Task<bool> DeleteTeam(Guid id);
    }
}
