namespace Data.Models
{
    public class TimeLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Hours { get; set; }

        public int UserProjectId { get; set; }

        public UserProject UserProject { get; set; } = null!;
    }
}
