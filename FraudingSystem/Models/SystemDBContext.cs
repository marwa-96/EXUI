using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Core.Objects;




namespace Ext_FraudingSystem.Models
{
    public class SystemDbContext : IdentityDbContext
    {
        public SystemDbContext()
            : base("DBContextModel")
        {
            Database.SetInitializer(new EntitiesContextInitializer());
        }

        public DbSet<FraudCases> FraudCases { get; set; }
        public DbSet<FraudCasesAttachments> FraudCasesAttachments { get; set; }
        public DbSet<FraudCaseTrace> FraudCaseTrace { get; set; }
        public DbSet<UsersPrivlidge> UsersPrivlidge { get; set; }
        public DbSet<FraudStatus> FraudStatus { get; set; }
        public DbSet<FraudEmployees> FraudEmployees { get; set; }
        public DbSet<LabelCases> LabelCases { get; set; }
        public DbSet<FraudIsCustomer> FraudIsCustomer { get; set; }
        public DbSet<FraudEmailSettings> FraudEmailSettings { get; set; }
        public DbSet<MonitoringLog> MonitoringLog { get; set; }
        public DbSet<CasesLog> CasesLog { get; set; }

        
        
        
        public static SystemDbContext Create()
        {
            return new SystemDbContext();
        }

        //protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<IdentityUser>().ToTable("Sys_Users").Property(p => p.Id).HasColumnName("UserId");
        //    modelBuilder.Entity<ApplicationUser>().ToTable("Sys_Users").Property(p => p.Id).HasColumnName("UserId");
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("Sys_UserRoles");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("Sys_UserLogins");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("Sys_UserClaims");
        //    modelBuilder.Entity<IdentityRole>().ToTable("Sys_Roles");
        //}


    }

}
