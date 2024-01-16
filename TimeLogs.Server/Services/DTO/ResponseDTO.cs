namespace Services.DTO
{
    public class ResponseDTO<T>
    {
        public ICollection<T> Data { get; set; } = new List<T>();

        public int TotalCount { get; set; }
    }
}
