namespace Api.Models
{
    public class ListResponse<T>
    {
        public ICollection<T> Data { get; set; } = new List<T>();

        public int TotalCount { get; set; }
    }
}
