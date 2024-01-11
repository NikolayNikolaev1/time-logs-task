namespace Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class TimeLogsDbContext : DbContext
    {
        public TimeLogsDbContext(DbContextOptions<TimeLogsDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TimeLog> TimeLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("name=DefaultConnection");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(up => up.UserId);

            builder
                .Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.Users)
                .HasForeignKey(up => up.ProjectId);

            builder
                .Entity<UserProject>()
                .HasMany(up => up.TimeLogs)
                .WithOne(tl => tl.UserProject)
                .HasForeignKey(tl => tl.UserProjectId);

            base.OnModelCreating(builder);
        }
    }
}
