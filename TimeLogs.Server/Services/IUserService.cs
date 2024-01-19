namespace Services
{
    using DTO;

    public interface IUserService
    {
        Task<ICollection<UserDTO>> AllAsync(DateTime? dateFrom, DateTime? dateTo);

        Task<bool> ContainsEmailAsync(string email);

        Task<int> CreateAsync(string firstName, string lastName, string email);

        Task DeleteAllAsync();

        Task<UserDTO> FindByIdAsync(int id, DateTime? dateFrom, DateTime? dateTo);

        Task<double> GetTotalWorkedHours(int userId, DateTime? date);
    }
}