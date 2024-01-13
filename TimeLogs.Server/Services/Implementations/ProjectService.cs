namespace Services.Implementations
{
    using Data;
    using Data.Models;
    using DTO;
    using Microsoft.EntityFrameworkCore;
    using System;
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
                // TODO: Add filter by dateRange in grandchildren collection.
                .Where(p => p.Users.Any(u => u.TimeLogs
                    .Any(tl => dateFrom != null && dateTo != null
                        ? tl.Date >= dateFrom && tl.Date <= dateTo
                        : true))
                )
                .Select(p => new ProjectDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    // TODO: Sum hours in time.
                    HoursWorked = Math.Round(p.Users.SelectMany(u => u.TimeLogs).Sum(tl => tl.Hours), 2)
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
