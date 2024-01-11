namespace Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<UserProject> Projects { get; set; } = new List<UserProject>();
    }
}
