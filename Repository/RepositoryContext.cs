using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {

        public RepositoryContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<FinancialRecord>? FinancialRecords { get; set; }
        public DbSet<Investigation>? Investigations { get; set; }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<PatientInvestigation>? PatientInvestigations { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }

    }
}
