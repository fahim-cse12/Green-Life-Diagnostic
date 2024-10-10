using Entities.Models;
using Entities.NonDbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System.Reflection;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {

        public RepositoryContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            // mark all non entity types in DBContext
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
            .Where(child => typeof(INonEntityBase).IsAssignableFrom(child) && child.IsClass && !child.IsAbstract))
            {
                modelBuilder.Entity(type).HasNoKey().ToView(null);
            }
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    // Disable cascade delete
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

                }
            }
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<FinancialRecord>? FinancialRecords { get; set; }
        public DbSet<Investigation>? Investigations { get; set; }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<PatientInvestigation>? PatientInvestigations { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<PatientInvestigationDetail>? PatientInvestigationDetails { get; set; }

    }
}
