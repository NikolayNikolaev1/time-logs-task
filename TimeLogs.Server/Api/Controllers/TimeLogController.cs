namespace Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.DTO;


    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private readonly ITimeLogService timeLogServce;

        public TimeLogController(ITimeLogService timeLogService) => 
            this.timeLogServce = timeLogService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponse<TimeLogDTO>))]
        public async Task<IActionResult> Get(
            [FromQuery(Name = "dateFrom")] string? from,
            [FromQuery(Name = "dateTo")] string? to,
            [FromQuery(Name = "page")] string page = "1")
        {
            DateTime? dateFrom = DateTime.TryParse(from, out _) ? DateTime.Parse(from) : null;
            DateTime? dateTo = DateTime.TryParse(to, out _) ? DateTime.Parse(to) : null;
            int pageNumber = int.TryParse(page, out pageNumber) ? pageNumber : 1;

            ICollection<TimeLogDTO> timeLogs = await this.timeLogServce.AllAsync(
                dateFrom?.ToUniversalTime().AddHours(2),
                dateTo?.ToUniversalTime().AddHours(2),
                pageNumber);

            int timeLogsCount = await this.timeLogServce.CountAsync(
                dateFrom?.ToUniversalTime().AddHours(2),
                dateTo?.ToUniversalTime().AddHours(2));

            return Ok(new ListResponse<TimeLogDTO>
            {
                Data = timeLogs,
                TotalCount = timeLogsCount
            });
        }
    }
}
