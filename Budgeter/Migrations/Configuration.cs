namespace Budgeter.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Budgeter.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Budgeter.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
              new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "HouseHoldAdmin"))
            {
                roleManager.Create(new IdentityRole { Name = "HouseHoldAdmin" });
            }
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                roleManager.Create(new IdentityRole { Name = "User" });
            }
            if (!context.Roles.Any(r => r.Name == "Guest"))
            {
                roleManager.Create(new IdentityRole { Name = "Guest" });
            }
            var userManager = new UserManager<ApplicationUser>(
                  new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "neenaaelias@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Neena",
                    LastName = "Aelias",
                    PhoneNumber = "(203) 550-7631",
                    UserName = "neenaaelias@gmail.com",
                    Email = "neenaaelias@gmail.com",
                }, "Password-1");
            }

            if (!context.Users.Any(u => u.Email == "thomsonin@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "thomson",
                    LastName =  "thomas",
                    PhoneNumber = "(203) 653-5040",
                    UserName = "thomsonin@gmail.com",
                    Email = "thomsonin@gmail.com",
                }, "Password-1");
            }

            if (!context.Users.Any(u => u.Email == "user@budgeter.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "user",
                    LastName = "User",
                    PhoneNumber = "(###) ###-####",
                    UserName = "user@budgeter.com",
                    Email = "user@budgeter.com",
                }, "Password-1");
            }



            if (!context.Users.Any(u => u.Email == "guest@budgeter.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Guest",
                    LastName = "Userguest",
                    PhoneNumber = "(###) ###-####",
                    UserName = "guest@budgeter.com",
                    Email = "guest@budgeter.com",
                }, "Password-1");
            }

            var userId_Neena = userManager.FindByEmail("neenaaelias@gmail.com").Id;
            userManager.AddToRole(userId_Neena, "HouseHoldAdmin");

            var userId_Tom = userManager.FindByEmail("thomsonin@gmail.com").Id;
            userManager.AddToRole(userId_Tom, "Admin");

            var userId_User = userManager.FindByEmail("user@budgeter.com").Id;
            userManager.AddToRole(userId_User, "User");

            var userId_Guest = userManager.FindByEmail("guest@budgeter.com").Id;
            userManager.AddToRole(userId_Guest, "Guest");


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
