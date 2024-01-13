namespace Services
{
    using DTO;

    public interface IProjectService
    {
        Task<ICollection<ProjectDTO>> AllAsync(DateTime? dateFrom, DateTime? dateTo);

        Task<int> CreateAsync(string name);

        Task DeleteAllAsync();
    }
}
