namespace Services.Implementations
{
    using Data;
    using Data.Models;
    using DTO;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly TimeLogsDbContext dbContext;

        public UserService(TimeLogsDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<ICollection<UserDTO>> AllAsync(DateTime? dateFrom, DateTime? dateTo)
        {
            return await this.dbContext
                .Users
                // TODO: Find a way to filter by date in grandchildren collection.
                .Where(u => u.Projects.Any(p => p.TimeLogs
                    .Any(tl => dateFrom != null && dateTo != null
                        ? tl.Date >= dateFrom && tl.Date <= dateTo
                        : true)))
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    // TODO: Sum hours in time.
                    HoursWorked = Math.Round(u.Projects.SelectMany(p => p.TimeLogs).Sum(tl => tl.Hours), 2)
                    //utl.TimeLogs.Aggregate(
                    //    TimeSpan.Zero,
                    //    (sumSoFar, nextMyObject) => sumSoFar + TimeSpan.FromHours(nextMyObject.Hours)).TotalHours
                    //new TimeSpan(utl.TimeLogs.Sum(tl => TimeSpan.FromHours(tl.Hours).Ticks)).TotalHours
                })
                .ToListAsync();
        }

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
