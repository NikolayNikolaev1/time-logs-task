namespace Services.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public double HoursWorked { get; set; }
    }
}
