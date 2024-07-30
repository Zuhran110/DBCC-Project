using Microsoft.EntityFrameworkCore;

namespace WpfApp6.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<ProjectEngineer> ProjectEngineers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Zuhran\\SQLEXPRESS;Database=WpfApp6;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEngineer>()
                .HasKey(pe => new { pe.ProjectId, pe.EngineerId });

            modelBuilder.Entity<ProjectEngineer>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEngineers)
                .HasForeignKey(pe => pe.ProjectId);

            modelBuilder.Entity<ProjectEngineer>()
                .HasOne(pe => pe.Engineer)
                .WithMany(e => e.ProjectEngineers)
                .HasForeignKey(pe => pe.EngineerId);
        }
    }
}
