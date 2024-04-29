using Huddle_CRUD.Core.Common;
using Huddle_CRUD.Core.Enum;
using Huddle_CRUD.Core.Filters;
using Huddle_CRUD.Core.ViewModels;
using Huddle_CRUD_app.Pages.Teams;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Huddle_CRUD_app.Services
{
    public class TeamService : ITeamService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly ToastService _toastService;
        public TeamService(IHttpClientFactory httpClientFactory, ToastService toastService)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("HuddleCrudApiClient");
            _toastService = toastService;
        }

        public async Task<TeamVm> GetTeamById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/team/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TeamVm>(content);
            }
            else
            {
                _toastService.ShowToast($"Failed to retrieve team. Status code: {response.StatusCode}", ToastLevel.Error,TimeSpan.FromSeconds(5));
                return null;
            }
        }

        public async Task<IEnumerable<TeamVm>> GetAllTeams()
        {
            var response = await _httpClient.GetAsync("api/team");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TeamVm>>(content);
            }
            else
            {
                _toastService.ShowToast($"Failed to retrieve team. Status code: {response.StatusCode}", ToastLevel.Error, TimeSpan.FromSeconds(5));
                return null;
            }
        }

        public async Task<PaginatedResponse<List<TeamVm>>> FindTeams(TeamFilter filter)
        {
            var response = await _httpClient.PostAsJsonAsync<TeamFilter>("api/team/find", filter);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaginatedResponse<List<TeamVm>>>(content);
            }
            else
            {
                _toastService.ShowToast($"Failed to retrieve team. Status code: {response.StatusCode}", ToastLevel.Error, TimeSpan.FromSeconds(5));
                return null;
            }
        }

        public async Task<bool> InsertTeam(TeamVm team)
        {
            var response = await _httpClient.PostAsJsonAsync<TeamVm>("api/team", team);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _toastService.ShowToast($"Failed to create team. Status code: {response.StatusCode}", ToastLevel.Error, TimeSpan.FromSeconds(5));
                return false;
            }
        }

        public async Task<bool> UpdateTeam(TeamVm team)
        {
            var response = await _httpClient.PutAsJsonAsync<TeamVm>($"api/team/{team.Id}", team);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _toastService.ShowToast($"Failed to update team. Status code: {response.StatusCode}", ToastLevel.Error, TimeSpan.FromSeconds(5));
                return false;
            }
        }

        public async Task<bool> DeleteTeam(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/team/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _toastService.ShowToast($"Failed to delete team. Status code: {response.StatusCode}", ToastLevel.Error, TimeSpan.FromSeconds(5));
                return false;
            }
        }
    }
}
