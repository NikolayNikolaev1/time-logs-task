namespace Services
{
    using DTO;

    public interface ITimeLogService
    {
        Task<ICollection<TimeLogDTO>> AllAsync(DateTime? dateFrom, DateTime? dateTo, int page);

        Task<bool> ContainsUserProjectDateAsync(int userId, int projectId, DateTime date);

        Task CreateAsync(int userId, int projectId, DateTime date, double hours);

        Task DeleteAllAsync();
    }
}
