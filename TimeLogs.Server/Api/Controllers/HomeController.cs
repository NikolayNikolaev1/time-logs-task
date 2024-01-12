namespace Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System;

    using static Core.Constants;

    public class HomeController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IProjectService projectService;
        private readonly ITimeLogService timeLogService;

        public HomeController(
            IUserService userService,
            IProjectService projectService,
            ITimeLogService timeLogService)
        {
            this.userService = userService;
            this.projectService = projectService;
            this.timeLogService = timeLogService;
        }

        [HttpDelete("~/api/Clear")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Clear()
        {
            await this.timeLogService.DeleteAllAsync();
            await this.userService.DeleteAllAsync();
            await this.projectService.DeleteAllAsync();

            return NoContent();
        }

        [HttpPost("~/api/Generate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Generate()
        {
            Random random = new Random();

            ICollection<int> projectIds = await this.GenerateProjectsAsync(PROJECTS_COUNT);

            await this.GenerateUserTimeLogsAsync(random, USERS_COUNT, projectIds);


            return NoContent();
        }

        private async Task<ICollection<int>> GenerateProjectsAsync(int projectsCount)
        {
            ICollection<int> projectIds = new List<int>();

            for (int i = 0; i < projectsCount; i++)
            {
                projectIds.Add(await this.projectService.CreateAsync(PROJECT_NAMES[i]));
            }

            return projectIds;
        }

        private async Task GenerateUserTimeLogsAsync(
            Random random,
            int usersCount,
            ICollection<int> projectIds)
        {
            int totalUsers = 0;

            while (totalUsers++ < usersCount)
            {
                string firstName = USER_FIRST_NAMES[random.Next(0, USER_FIRST_NAMES.Length)];
                string lastName = USER_LAST_NAMES[random.Next(0, USER_LAST_NAMES.Length)];
                string emailDomain = USER_EMAIL_DOMAINS[random.Next(0, USER_EMAIL_DOMAINS.Length)];
                string email = $"{firstName.ToLower()}.{lastName.ToLower()}@{emailDomain}";
                int userProjectsCount = random.Next(USER_PROJECTS_MIN_COUNT, USER_PROJECTS_MAX_COUNT);

                int userId = await this.userService.CreateAsync(firstName, lastName, email);

                await this.GenerateTimeLogsAsync(random, userProjectsCount, projectIds, userId);
            }
        }

        private async Task GenerateTimeLogsAsync(
            Random random,
            int userProjectsCount,
            ICollection<int> projectIds,
            int userId)
        {
            while (userProjectsCount > 0)
            {
                int projectId = projectIds.ElementAt(random.Next(0, projectIds.Count));
                double hoursWorked = Math.Round(
                random.NextDouble() * (MAX_HOURS_PER_DATE - MIN_HOURS_PER_DATE) + MIN_HOURS_PER_DATE, 2);

                DateTime start = new DateTime(2024, 1, 1);
                int range = (DateTime.Today - start).Days;
                DateTime date = start.AddDays(random.Next(range));
                // Skip this record if relation between UserProject and current selected date already exists.
                if (await this.timeLogService.ContainsUserProjectDateAsync(userId, projectId, date)) continue;

                double hoursForCurrentDate = await this.userService.GetTotalWorkedHours(userId, date);
                /* In case of exceeding the maximum working hours of 8,
                 *  create the new record with the remaining allowed hours. Skip current record if
                 *  the maximum amount of 8 is reached or near by 0.25h(minimum per date). */
                if (hoursForCurrentDate + hoursWorked > MAX_HOURS_PER_DATE)
                {
                    hoursWorked = MAX_HOURS_PER_DATE - hoursForCurrentDate;
                }

                if (hoursWorked < MIN_HOURS_PER_DATE) continue;
                // Rounds to the bigger hour if it exceeds 59 minutes.
                if (hoursWorked - Math.Truncate(hoursWorked) > 0.5) hoursWorked = Math.Ceiling(hoursWorked);
                
                await this.timeLogService.CreateAsync(userId, projectId, date, hoursWorked);

                userProjectsCount--;
            }
        }
    }
}
