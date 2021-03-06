using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities.EmployeeReservation;
using Models.Entities.Factor;
using Models.Entities.Score;
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
        public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        #endregion

        #region Jobs

        public DbSet<JobCategory> jobCategories { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        #endregion

        #region Reservation

        public DbSet<HourReservation> HourReservation { get; set; }
        public DbSet<DataReservation> DataReservation { get; set; }
        public DbSet<ReservationStatus> ReservationStatus { get; set; }
        public DbSet<UserReserveStatus> UserReserveStatus { get; set; }
        public DbSet<ReservationOrder> ReservationOrders { get; set; }

        #endregion

        #region Factor

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Invoicing> Invoicings { get; set; }
        public DbSet<InvoicingDetail> InvoicingDetails { get; set; }
        public DbSet<FinancialTransactionStatus> FinancialTransactionStatus { get; set; }
        public DbSet<FinancialTrnsaction> FinancialTrnsactions { get; set; }
        public DbSet<AdminWallet> AdminWallet { get; set; }
        public DbSet<EmployeeWallet> EmployeeWallet { get; set; }
        public DbSet<RequestForCheckoutStatus> RequestForCheckoutStatus { get; set; }
        public DbSet<RequestForCheckout> RequestForCheckout { get; set; }

        #endregion

        #region Scores

        public DbSet<Models.Entities.Score.Scores> Scores { get; set; }

        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            #region User

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

            modelBuilder.Entity<Comment>()
                 .HasOne(x => x.Employee)
                 .WithMany(x => x.EmployeeComment)
                 .HasForeignKey(x => x.EmployeeID);

            modelBuilder.Entity<Comment>()
             .HasOne(x => x.User)
             .WithMany(x => x.UserComment)
             .HasForeignKey(x => x.UserID);
            #endregion

            #region ReservationOrder

            modelBuilder.Entity<ReservationOrder>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.ReservationOrderEmployee)
            .HasForeignKey(x => x.EmployeeID);

            modelBuilder.Entity<ReservationOrder>()
             .HasOne(x => x.User)
             .WithMany(x => x.ReservationOrderUser)
             .HasForeignKey(x => x.UserID);

            modelBuilder.Entity<ReservationOrder>()
                .HasQueryFilter(u => !u.IsDelete);
            #endregion

            #region Factor

            modelBuilder.Entity<FinancialTrnsaction>()
                      .HasOne(x => x.Employee)
                      .WithMany(x => x.FinancialTrnsactionsEmployee)
                      .HasForeignKey(x => x.EmployeeID);

            modelBuilder.Entity<FinancialTrnsaction>()
             .HasOne(x => x.User)
             .WithMany(x => x.FinancialTrnsactionsUser)
             .HasForeignKey(x => x.UserID);

            modelBuilder.Entity<Invoicing>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.InvoicingEmployee)
            .HasForeignKey(x => x.EmployeeID);

            modelBuilder.Entity<Invoicing>()
             .HasOne(x => x.User)
             .WithMany(x => x.InvoicingUser)
             .HasForeignKey(x => x.UserID);

            modelBuilder.Entity<Invoicing>()
           .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Tariff>()
              .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Invoicing>()
                  .HasQueryFilter(u => !u.IsDelete);

            #endregion

            #region Scores

            modelBuilder.Entity<Scores>()
               .HasOne(x => x.Employee)
               .WithMany(x => x.ScoresEmployee)
               .HasForeignKey(x => x.EmployeeID);

            modelBuilder.Entity<Scores>()
             .HasOne(x => x.User)
             .WithMany(x => x.ScoresUser)
             .HasForeignKey(x => x.UserID);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #endregion



    }
}
