namespace Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.DTO;

    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
            => this.projectService = projectService;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<UserDTO>))]
        public async Task<IActionResult> Get(
            [FromQuery(Name = "dateFrom")] string? from,
            [FromQuery(Name = "dateTo")] string? to)
        {
            DateTime? dateFrom = DateTime.TryParse(from, out _) ? DateTime.Parse(from) : null;
            DateTime? dateTo = DateTime.TryParse(to, out _) ? DateTime.Parse(to) : null;

            return Ok(await this.projectService.AllAsync(
                dateFrom?.ToUniversalTime().AddHours(2),
                dateTo?.ToUniversalTime().AddHours(2)
                ));
        }
    }
}
