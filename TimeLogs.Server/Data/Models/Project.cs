namespace Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public IEnumerable<UserProject> Users { get; set; } = new List<UserProject>();
    }
}
