namespace Services.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProjectService : IProjectService
    {
        private readonly TimeLogsDbContext dbContext;

        public ProjectService(TimeLogsDbContext dbContext)
            => this.dbContext = dbContext;

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
