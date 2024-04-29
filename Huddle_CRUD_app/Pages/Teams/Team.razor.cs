using Huddle_CRUD.Core.Common;
using Huddle_CRUD.Core.Enum;
using Huddle_CRUD.Core.Filters;
using Huddle_CRUD.Core.ViewModels;
using Huddle_CRUD_app.Services;
using Microsoft.AspNetCore.Components;

namespace Huddle_CRUD_app.Pages.Teams
{
    public partial class Team : ComponentBase
    {
        [Inject]
        protected ITeamService TeamService { get; set; }
        [Inject] ToastService ToastService { get; set; }
        protected PaginatedResponse<List<TeamVm>> teams { get; set; }
        protected TeamFilter teamFilter = new TeamFilter() { Page = 1, PageSize = 5 };
        protected bool addingTeam = false;
        protected string newTeamName = "";
        private TimeSpan duration = TimeSpan.FromSeconds(3);

        protected async override Task OnInitializedAsync()
        {
            await FindTeams(teamFilter);
        }

        protected async Task FindTeams(TeamFilter filter)
        {
            teams = await TeamService.FindTeams(filter);
            StateHasChanged();
        }

        protected async Task InsertTeam(string name)
        {
            var success = await TeamService.InsertTeam(new TeamVm { Name = name });
            if (success)
            {
                newTeamName = "";
                addingTeam = false;
                await FindTeams(teamFilter);
                ShowNotification("Created!", ToastLevel.Success, duration);
            }
        }

        protected async Task UpdateTeam(TeamVm teamVm)
        {
            var success = await TeamService.UpdateTeam(teamVm);
            if (success)
            {
                ToggleEdit(teamVm);
                ShowNotification("Updated!", ToastLevel.Success, duration);
            }
        }

        protected async Task DeleteTeam(Guid id)
        {
            var success = await TeamService.DeleteTeam(id);
            if (success)
            {
                teamFilter.Page = 1;
                await FindTeams(teamFilter);
                ShowNotification("Deleted!", ToastLevel.Success, duration);
            }
        }

        protected async void OnPageSizeChange()
        {
            teamFilter.Page = 1;
            await FindTeams(teamFilter);
        }

        protected async void ChangePage(int page)
        {
            teamFilter.Page = page;
            await FindTeams(teamFilter);
        }

        protected void ToggleAddTeam()
        {
            newTeamName = "";
            addingTeam = !addingTeam;
        }

        protected void ToggleEdit(TeamVm team, bool? cancel = null)
        {
            if (cancel.HasValue && cancel.Value)
                team.Name = team.InitialName;

            team.EditMode = !team.EditMode;
        }

        protected void ClearFilter() => teamFilter = new TeamFilter() { Page = 1, PageSize = 5 };

        protected void ShowNotification(string message, ToastLevel type, TimeSpan duration)
        {
            ToastService.ShowToast(message, type, duration);
        }

        protected bool BeforeUpdateCheck(TeamVm team)
        {
            return team.Name.Equals(team.InitialName) || string.IsNullOrEmpty(team.Name);
        }
    }
}
