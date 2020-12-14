using Microsoft.EntityFrameworkCore;
using ProviderApps.Core.Models;

namespace ProviderApps.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientInsurance> PatientInsurances { get; set; }
           
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
