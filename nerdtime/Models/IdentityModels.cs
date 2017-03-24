﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace nerdtime.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<nerdtime.Models.ConsoleType> ConsoleTypes { get; set; }

        public System.Data.Entity.DbSet<nerdtime.Models.Game> Games { get; set; }

        public System.Data.Entity.DbSet<nerdtime.Models.GameCategory> GameCategories { get; set; }

        public System.Data.Entity.DbSet<nerdtime.Models.GameConsoleType> GameConsoleTypes    { get; set; }


       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>().HasOptional(g => g.Consoles).WithMany().Map(m => m.MapKey("ConsoleTypesId"));
        }*/


    }
}