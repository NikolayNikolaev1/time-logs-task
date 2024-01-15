namespace Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.DTO;

    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
            => this.userService = userService;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<UserDTO>))]
        public async Task<IActionResult> Get(
            [FromQuery(Name = "dateFrom")] string? from,
            [FromQuery(Name = "dateTo")] string? to)
        {
            DateTime? dateFrom = DateTime.TryParse(from, out _) ? DateTime.Parse(from) : null;
            DateTime? dateTo = DateTime.TryParse(to, out _) ? DateTime.Parse(to) : null;

            return Ok(await this.userService.AllAsync(
                dateFrom?.ToUniversalTime().AddHours(2),
                dateTo?.ToUniversalTime().AddHours(2)));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        public async Task<IActionResult> Get(
            int id,
            [FromQuery(Name = "dateFrom")] string? from,
            [FromQuery(Name = "dateTo")] string? to)
        {
            DateTime? dateFrom = DateTime.TryParse(from, out _) ? DateTime.Parse(from) : null;
            DateTime? dateTo = DateTime.TryParse(to, out _) ? DateTime.Parse(to) : null;

            return Ok(await this.userService.FindByIdAsync(
                id,
                dateFrom?.ToUniversalTime().AddHours(2),
                dateTo?.ToUniversalTime().AddHours(2)));
        }
    }
}
