namespace Services
{
    public interface IUserService
    {
        Task<int> CreateAsync(string firstName, string lastName, string email);

        Task DeleteAllAsync();

        Task<double> GetTotalWorkedHours(int userId, DateTime? date);
    }
}