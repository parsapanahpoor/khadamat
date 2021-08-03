﻿// <auto-generated />
using System;
using DataContext.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataContext.Migrations
{
    [DbContext(typeof(KhadamatContext))]
    partial class KhadamatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Models.Entities.EmployeeReservation.DataReservation", b =>
                {
                    b.Property<int>("DataReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ReservationDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("DataReservationID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("DataReservation");
                });

            modelBuilder.Entity("Models.Entities.EmployeeReservation.HourReservation", b =>
                {
                    b.Property<int>("HourReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DataReservationID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EndHourReservation")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("EndHourReservationInt")
                        .HasColumnType("int");

                    b.Property<int>("ReservationStatusID")
                        .HasColumnType("int");

                    b.Property<string>("StartHourReservation")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("StartHourReservationInt")
                        .HasColumnType("int");

                    b.HasKey("HourReservationID");

                    b.HasIndex("DataReservationID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("ReservationStatusID");

                    b.ToTable("HourReservation");
                });

            modelBuilder.Entity("Models.Entities.EmployeeReservation.ReservationStatus", b =>
                {
                    b.Property<int>("ReservationStatusID")
                        .HasColumnType("int");

                    b.Property<string>("EnglishStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersianStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReservationStatusID");

                    b.ToTable("ReservationStatus");
                });

            modelBuilder.Entity("Models.Entities.User.EmployeeDocuments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthdayLocation")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long?>("CertificateCode")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeCertificate")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FamilyName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FieldOfStudy")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("HealthStatus")
                        .HasColumnType("bit");

                    b.Property<string>("HealthStatusDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("HistoryOfDetention")
                        .HasColumnType("bit");

                    b.Property<string>("HistoryOfDetentionDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("JobRank")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastEducationalCertificate")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastWorkExperience")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("MaritalStatus")
                        .HasColumnType("bit");

                    b.Property<bool?>("MilitaryService")
                        .HasColumnType("bit");

                    b.Property<string>("MilitaryServiceDesCription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("Obligation")
                        .HasColumnType("bit");

                    b.Property<long?>("PersonalCode")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PersonalCodeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonalCodeLocation")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PersonalPicture")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("PossitionId")
                        .HasColumnType("int");

                    b.Property<string>("Religion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Userid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PossitionId")
                        .IsUnique();

                    b.HasIndex("Userid")
                        .IsUnique();

                    b.ToTable("employeeDocument");
                });

            modelBuilder.Entity("Models.Entities.User.EmployeeInformationPossition", b =>
                {
                    b.Property<int>("PossitionId")
                        .HasColumnType("int");

                    b.Property<string>("PossitionName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PossitionId");

                    b.ToTable("employeeInformationPossitions");
                });

            modelBuilder.Entity("Models.Entities.User.EmployeeStatus", b =>
                {
                    b.Property<int>("EmployeeStatusID")
                        .HasColumnType("int");

                    b.Property<string>("EnglishStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersianStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeStatusID");

                    b.ToTable("EmployeeStatuses");
                });

            modelBuilder.Entity("Models.Entities.User.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LocationAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LocationID");

                    b.HasIndex("UserID");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("Models.Entities.User.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ActiveCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("EmployeeStatusID")
                        .HasColumnType("int");

                    b.Property<string>("ForgotPasswordCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserAvatar")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeStatusID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Models.Entities.User.UserSelectedJob", b =>
                {
                    b.Property<int>("JobCategorySelectedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JobCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ResumeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAvatar")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("JobCategorySelectedID");

                    b.HasIndex("JobCategoryId");

                    b.HasIndex("Userid");

                    b.ToTable("UserSelectedJobs");
                });

            modelBuilder.Entity("Models.Entities.Works.JobCategory", b =>
                {
                    b.Property<int>("JobCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("JobLogo")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("Percent")
                        .HasColumnType("int");

                    b.HasKey("JobCategoryId");

                    b.ToTable("jobCategories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Models.Entities.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Models.Entities.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entities.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Models.Entities.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Entities.EmployeeReservation.DataReservation", b =>
                {
                    b.HasOne("Models.Entities.User.User", "User")
                        .WithMany("DataReservation")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Entities.EmployeeReservation.HourReservation", b =>
                {
                    b.HasOne("Models.Entities.EmployeeReservation.DataReservation", "DataReservation")
                        .WithMany("HourReservation")
                        .HasForeignKey("DataReservationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Entities.User.User", "User")
                        .WithMany("HourReservation")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Entities.EmployeeReservation.ReservationStatus", "ReservationStatus")
                        .WithMany("HourReservations")
                        .HasForeignKey("ReservationStatusID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DataReservation");

                    b.Navigation("ReservationStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Entities.User.EmployeeDocuments", b =>
                {
                    b.HasOne("Models.Entities.User.EmployeeInformationPossition", "EmployeeInformationPossition")
                        .WithOne("EmployeeDocuments")
                        .HasForeignKey("Models.Entities.User.EmployeeDocuments", "PossitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entities.User.User", "User")
                        .WithOne("EmployeeDocuments")
                        .HasForeignKey("Models.Entities.User.EmployeeDocuments", "Userid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EmployeeInformationPossition");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Entities.User.Location", b =>
                {
                    b.HasOne("Models.Entities.User.User", "User")
                        .WithMany("Location")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Entities.User.User", b =>
                {
                    b.HasOne("Models.Entities.User.EmployeeStatus", "EmployeeStatus")
                        .WithMany("User")
                        .HasForeignKey("EmployeeStatusID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EmployeeStatus");
                });

            modelBuilder.Entity("Models.Entities.User.UserSelectedJob", b =>
                {
                    b.HasOne("Models.Entities.Works.JobCategory", "JobCategory")
                        .WithMany("UserSelected")
                        .HasForeignKey("JobCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Entities.User.User", "User")
                        .WithMany("UserSelected")
                        .HasForeignKey("Userid");

                    b.Navigation("JobCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Entities.EmployeeReservation.DataReservation", b =>
                {
                    b.Navigation("HourReservation");
                });

            modelBuilder.Entity("Models.Entities.EmployeeReservation.ReservationStatus", b =>
                {
                    b.Navigation("HourReservations");
                });

            modelBuilder.Entity("Models.Entities.User.EmployeeInformationPossition", b =>
                {
                    b.Navigation("EmployeeDocuments");
                });

            modelBuilder.Entity("Models.Entities.User.EmployeeStatus", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Entities.User.User", b =>
                {
                    b.Navigation("DataReservation");

                    b.Navigation("EmployeeDocuments");

                    b.Navigation("HourReservation");

                    b.Navigation("Location");

                    b.Navigation("UserSelected");
                });

            modelBuilder.Entity("Models.Entities.Works.JobCategory", b =>
                {
                    b.Navigation("UserSelected");
                });
#pragma warning restore 612, 618
        }
    }
}
