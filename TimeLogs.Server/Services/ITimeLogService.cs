namespace Services
{
    public interface ITimeLogService
    {
        Task<bool> ContainsUserProjectDateAsync(int userId, int projectId, DateTime date);

        Task CreateAsync(int userId, int projectId, DateTime date, double hours);

        Task DeleteAllAsync();
    }
}
