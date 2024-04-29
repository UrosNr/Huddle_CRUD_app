using Huddle_CRUD.Core.Common;
using Huddle_CRUD.Core.Filters;
using Huddle_CRUD.Core.Models;
using Huddle_CRUD.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Huddle_CRUD_Api.Repositories
{
    public interface ITeamRepository
    {
        Task<TeamModel> GetTeamById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TeamModel>> GetAllTeams(CancellationToken cancellationToken);
        Task<PaginatedResponse<List<TeamModel>>> FindTeam(TeamFilter filter, CancellationToken cancellationToken);
        Task<bool> InsertTeam(TeamVm team, CancellationToken cancellationToken);
        Task<bool> UpdateTeam(TeamVm team, CancellationToken cancellationToken);
        Task<bool> DeleteTeam(Guid id, CancellationToken cancellationToken);
    }
}
