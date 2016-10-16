using Lab2.Models;
using Lab2.Models.DbModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab2.DAL.Contexts
{
        public class IdentityContext : IdentityDbContext<ApplicationUser>
        {
            public IdentityContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }
            public static IdentityContext Create()
            {
                return new IdentityContext();
            }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}