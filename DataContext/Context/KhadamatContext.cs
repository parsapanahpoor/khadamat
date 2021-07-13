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

        public DbSet<UserProfile> UserProfile { get; set; }


        #endregion


        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<UserProfile>(table =>
            {
                table.HasKey(x => x.UserAvatarId);

                table.HasOne(x => x.User)
                .WithOne(x => x.UserProfile)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>()
           .HasQueryFilter(u => !u.IsDelete);










            base.OnModelCreating(modelBuilder);
        }

        #endregion



    }
}
