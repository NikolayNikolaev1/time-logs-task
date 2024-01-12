namespace Services.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class TimeLogService : ITimeLogService
    {
        private readonly TimeLogsDbContext dbContext;

        public TimeLogService(TimeLogsDbContext dbContext)
            => this.dbContext = dbContext;

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
