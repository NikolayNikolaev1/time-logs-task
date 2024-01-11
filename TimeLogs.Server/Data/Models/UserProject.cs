﻿namespace Data.Models
{
    public class UserProject
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public IEnumerable<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}
