namespace Services.Implementations
{
    using Data;
    using Data.Models;
    using DTO;
    using Microsoft.EntityFrameworkCore;
    using System;

    using static Core.Constants;

    public class TimeLogService : ITimeLogService
    {
        private readonly TimeLogsDbContext dbContext;

        public TimeLogService(TimeLogsDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<ICollection<TimeLogDTO>> AllAsync(DateTime? dateFrom, DateTime? dateTo, int page)
        {
            return await this.dbContext
                .TimeLogs
                .Where(tl => dateFrom != null && dateTo != null 
                    ? tl.Date >= dateFrom && tl.Date <= dateTo
                    : true)
                .OrderBy(tl => tl.Date)
                .Skip(((page - 1) * PER_PAGE_COUNT))
                .Take(PER_PAGE_COUNT)
                .Select(tl => new TimeLogDTO
                    {
                        Id = tl.Id,
                        UserId = tl.UserProject.UserId,
                        UserFirstName = tl.UserProject.User.FirstName,
                        UserLastName = tl.UserProject.User.LastName,
                        UserEmail = tl.UserProject.User.Email,
                        ProjectName = tl.UserProject.Project.Name,
                        Date = tl.Date,
                        Hours = tl.Hours
                    })
                .ToListAsync();
        }

        public async Task<bool> ContainsUserProjectDateAsync(int userId, int projectId, DateTime date)
        {
            UserProject? userProject = await this.dbContext
                .UsersProjects
                .FirstOrDefaultAsync(up => up.UserId == userId && up.ProjectId == projectId);

            if (userProject == null) return false;

            return await this.dbContext
                .TimeLogs
                .AnyAsync(tl => tl.UserProjectId == userProject.Id && tl.Date == date);
        }

        public async Task<int> CountAsync(DateTime? dateFrom, DateTime? dateTo)
        {
            return await this.dbContext
                .TimeLogs
                .Where(tl => dateFrom != null && dateTo != null
                    ? tl.Date >= dateFrom && tl.Date <= dateTo
                    : true)
                .CountAsync();
        }

        public async Task CreateAsync(int userId, int projectId, DateTime date, double hours)
        {
            UserProject? userProject = await this.dbContext
                .UsersProjects
                .FirstOrDefaultAsync(up => up.UserId == userId && up.ProjectId == projectId);

            if (userProject == null)
            {
                userProject = new UserProject { UserId = userId, ProjectId = projectId };

                await this.dbContext.UsersProjects.AddAsync(userProject);
                await this.dbContext.SaveChangesAsync();
            }

            await this.dbContext.TimeLogs.AddAsync(new TimeLog
            {
                UserProjectId = userProject.Id,
                Date = date,
                Hours = hours
            });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await this.dbContext.TimeLogs.ExecuteDeleteAsync();
            await this.dbContext.UsersProjects.ExecuteDeleteAsync();
        }
    }
}
