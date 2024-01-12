namespace Services
{
    public interface IProjectService
    {
        Task<int> CreateAsync(string name);

        Task DeleteAllAsync();
    }
}
