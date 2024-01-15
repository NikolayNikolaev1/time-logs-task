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
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    HoursWorked = u.Projects.SelectMany(p => p.TimeLogs.Select(tl => new
                    {
                        tl.Id,
                        tl.Date,
                        tl.Hours
                    }))
                    .Where(tl => dateFrom != null && dateTo != null
                        ? tl.Date >= dateFrom && tl.Date <= dateTo
                        : true)
                    .Sum(tl => tl.Hours)
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

        public async Task<UserDTO> FindByIdAsync(int id, DateTime? dateFrom, DateTime? dateTo)
        {
            return await this.dbContext
                .Users
                .Where(u => u.Id == id)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    HoursWorked = u.Projects.SelectMany(p => p.TimeLogs.Select(tl => new
                    {
                        tl.Id,
                        tl.Date,
                        tl.Hours
                    }))
                    .Where(tl => dateFrom != null && dateTo != null
                        ? tl.Date >= dateFrom && tl.Date <= dateTo
                        : true)
                    .Sum(tl => tl.Hours)
                })
                .FirstAsync();
                
        }

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
