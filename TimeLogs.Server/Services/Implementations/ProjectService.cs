namespace Services.Implementations
{
    using Data;
    using Data.Models;
    using DTO;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ProjectService : IProjectService
    {
        private readonly TimeLogsDbContext dbContext;

        public ProjectService(TimeLogsDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<ICollection<ProjectDTO>> AllAsync(DateTime? dateFrom, DateTime? dateTo)
        {
            return await this.dbContext
                .Projects
                .Select(p => new ProjectDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    HoursWorked = p.Users.SelectMany(u => u.TimeLogs.Select(tl => new
                    {
                        tl.Id,
                        tl.Date,
                        tl.Hours
                    }))
                    .Where(tl => dateFrom != null && dateTo != null
                        ? tl.Date >= dateFrom && tl.Date <= dateTo
                        : true)
                    .Sum(tl => tl.Hours)
                    // TODO: Sum hours and minutes correctly.
                    //.Aggregate(
                    //    TimeSpan.Zero,
                    //    (sumSoFar, nextMyObject) => sumSoFar + TimeSpan.FromHours(nextMyObject.Hours)).TotalHours
                })
                .ToListAsync();
        }

        public async Task<int> CreateAsync(string name)
        {
            Project project = new Project { Name = name };

            await this.dbContext.Projects.AddAsync(project);
            await this.dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task DeleteAllAsync()
            => await this.dbContext.Projects.ExecuteDeleteAsync();
    }
}
