using AutoMapper;
using Huddle_CRUD.Core.Common;
using Huddle_CRUD.Core.Filters;
using Huddle_CRUD.Core.ViewModels;
using Huddle_CRUD_Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Huddle_CRUD_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public TeamController(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }
        // GET: api/<TeamController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamVm>>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await _teamRepository.GetAllTeams(cancellationToken);
            if (entities is not null) {
                return Ok(_mapper.Map<List<TeamVm>>(entities));
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamVm>> GetTeamById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _teamRepository.GetTeamById(id, cancellationToken);
            if (entity is not null)
            {
                return Ok(_mapper.Map<TeamVm>(entity));
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<TeamController>/find
        [HttpPost("find")]
        public async Task<ActionResult<PaginatedResponse<TeamVm>>> FindTeam([FromBody] TeamFilter filter, CancellationToken cancellationToken)
        {
            var result = await _teamRepository.FindTeam(filter, cancellationToken);
            if (result is not null)
            {
                var response = new PaginatedResponse<List<TeamVm>>()
                {
                    CurrentPage = result.CurrentPage,
                    CurrentPageSize = result.CurrentPageSize,
                    Result = _mapper.Map<List<TeamVm>>(result.Result),
                    TotalResultCount = result.TotalResultCount
                };
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<TeamController>
        [HttpPost]
        public async Task<ActionResult> InsertTeam([FromBody] TeamVm teamVm, CancellationToken cancellationToken)
        {
            var result = await _teamRepository.InsertTeam(teamVm, cancellationToken);
            return result ? Ok() : BadRequest();
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeam([FromBody] TeamVm teamVm, CancellationToken cancellationToken)
        {
            var result = await _teamRepository.UpdateTeam(teamVm, cancellationToken);
            return result ? Ok() : NotFound();
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(Guid id, CancellationToken cancellationToken)
        {
            var result = await _teamRepository.DeleteTeam(id, cancellationToken);
            return result ? Ok() : NotFound();
        }
    }
}
