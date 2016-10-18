using Lab2.Models;
using Lab2.Models.DbModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Collections;

namespace Lab2.DAL.Contexts
{
        public class IdentityContext : IdentityDbContext<ApplicationUser>
        {
            public IdentityContext() : base("DefaultConnection", throwIfV1Schema: false)
            {
                Database.SetInitializer<IdentityContext>(new CreateDatabaseIfNotExists<IdentityContext>());
            }
            public static IdentityContext Create()
            {
                return new IdentityContext();
            }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUserMessage> ApplicationUserMessages { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}