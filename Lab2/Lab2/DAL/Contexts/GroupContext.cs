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
    public class GroupContext : IdentityDbContext<ApplicationUser>
    {
        public GroupContext(): base("DefaultConnection")
        {
            Database.SetInitializer<GroupContext>(new CreateDatabaseIfNotExists<GroupContext>());
        }
        public DbSet<Group> Groups { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}