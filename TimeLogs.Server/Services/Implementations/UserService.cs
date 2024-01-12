namespace Services.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class UserService : IUserService
    {
        private readonly TimeLogsDbContext dbContext;

        public UserService(TimeLogsDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<int> CreateAsync(string firstName, string lastName, string email)
        {
            User user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAllAsync()
            => await this.dbContext.Users.ExecuteDeleteAsync();

        public async Task<double> GetTotalWorkedHours(int userId, DateTime? date)
        {
            User user = await this.dbContext.Users.FirstAsync(u => u.Id == userId);

            return user.Projects.Sum(p => date != null
                    ? p.TimeLogs.Any(tl => tl.Date == date) // TODO: Find better solution.
                        ? p.TimeLogs.First(tl => tl.Date == date).Hours
                        : 0.0
                    : p.TimeLogs.Sum(tl => tl.Hours));
        }
    }
}
