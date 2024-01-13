namespace Services.DTO
{
    public class TimeLogDTO
    {
        public int Id { get; set; }

        public string UserFirstName { get; set; } = null!;

        public string UserLastName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string ProjectName { get; set; } = null!;

        public DateTime Date { get; set; }

        public double Hours { get; set; }
    }
}
