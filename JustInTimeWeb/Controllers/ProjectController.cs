using Application.Account.Queries;
using Application.Projec.Commands;
using Application.Project.Commands;
using Application.Project.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustInTimeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseApiController
    {
        #region project CRUD

        [HttpPost("create")]
        public async Task<ActionResult> CreateProject(CreateProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> LoadProject(int id)
        {
            var result = await Mediator.Send(new ProjectQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //returns only projects that are marked as active
        [HttpGet("projects")]
        public async Task<ActionResult> GetActiveProjects()
        {
            var result = await Mediator.Send(new ProjectsQuery());
            if (result == null)
            {
                return Ok();
            }
            return Ok(result);
        }

        [HttpGet("archived")]
        public async Task<ActionResult> GetArchivedProjects()
        {
            var result = await Mediator.Send(new ProjectsQuery(false));
            if (result == null)
            {
                return Ok();
            }
            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<ActionResult> UpdateProject(UpdateProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteProject(DeleteProjectCommand command) //finds project by ID
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        #endregion project CRUD

        [HttpPost("addhours")]
        public async Task<ActionResult> AddHours(AddHoursToProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // Returns total hours worked on project
        [HttpGet("hours/{projectId}")]
        public async Task<ActionResult> GetHoursForProject(int projectId)
        {
            var result = await Mediator.Send(new HoursQuery(projectId));

            return Ok(result);
        }

        // Deactivate project
        [HttpPost("deactivate")]
        public async Task<ActionResult> DeactivateProject(DeactivateProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // Activate project
        [HttpPost("activate")]
        public async Task<ActionResult> ActivateProject(ActivateProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}