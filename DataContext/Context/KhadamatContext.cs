using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
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



            base.OnModelCreating(modelBuilder);
        }

        #endregion



    }
}
