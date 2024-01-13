namespace Services
{
    using DTO;

    public interface IUserService
    {
        Task<ICollection<UserDTO>> AllAsync(DateTime? dateFrom, DateTime? dateTo);

        Task<int> CreateAsync(string firstName, string lastName, string email);

        Task DeleteAllAsync();

        Task<double> GetTotalWorkedHours(int userId, DateTime? date);
    }
}