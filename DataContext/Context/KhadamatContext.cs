using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Context
{
    public class KhadamatContext : IdentityDbContext<User>
    {
        public KhadamatContext(DbContextOptions<KhadamatContext> options)
        : base(options)
        {

        }

        #region User

        public DbSet<EmployeeDocuments> employeeDocument { get; set; }
        public DbSet<EmployeeInformationPossition> employeeInformationPossitions { get; set; }
        public DbSet<UserSelectedJob> UserSelectedJobs { get; set; }

        #endregion
        #region Jobs

        public DbSet<JobCategory> jobCategories { get; set; }

        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>()
           .HasQueryFilter(u => !u.IsDelete);



            modelBuilder.Entity<EmployeeDocuments>()
             .HasOne(a => a.User)
             .WithOne(a => a.EmployeeDocuments)
             .HasForeignKey<EmployeeDocuments>(c => c.Userid);




            modelBuilder.Entity<EmployeeDocuments>()
             .HasOne(a => a.EmployeeInformationPossition)
             .WithOne(a => a.EmployeeDocuments)
             .HasForeignKey<EmployeeDocuments>(c => c.PossitionId);

            modelBuilder.Entity<JobCategory>()
           .HasQueryFilter(u => !u.IsDelete);



            base.OnModelCreating(modelBuilder);
        }

        #endregion



    }
}
