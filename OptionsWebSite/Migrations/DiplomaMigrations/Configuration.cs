namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using DiplomaDataModel.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.Models.DiplomaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\DiplomaMigrations";
        }

        protected override void Seed(DiplomaDataModel.Models.DiplomaContext context)
        {
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //if (!roleManager.RoleExists("Admin"))
            //    roleManager.Create(new IdentityRole("Admin"));
            //if (!roleManager.RoleExists("Student"))
            //    roleManager.Create(new IdentityRole("Student"));
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //string[] emails = { "a@a.a", "s@s.s" };
            //string[] users = { "A00111111", "A00222222" };

            //if (userManager.FindByEmail(emails[0]) == null)
            //{
            //    var user = new ApplicationUser
            //    {
            //        Email = emails[0],
            //        UserName = users[0],
            //    };
            //    var result = userManager.Create(user, "P@$$w0rd");
            //    if (result.Succeeded)
            //        userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Admin");
            //}
            //if (userManager.FindByEmail(emails[1]) == null)
            //{
            //    var user = new ApplicationUser
            //    {
            //        Email = emails[1],
            //        UserName = users[1],
            //    };
            //    var result = userManager.Create(user, "P@$$w0rd");
            //    if (result.Succeeded)
            //        userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Student");
            //}

            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 1, Year = 2015, Term = 20, IsDefault = false });
            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 2, Year = 2015, Term = 30, IsDefault = false });
            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 3, Year = 2016, Term = 10, IsDefault = false });
            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 4, Year = 2016, Term = 30, IsDefault = true });

            context.SaveChanges();

            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 1, Title = "Data Communications", IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 2, Title = "Client Server", IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 3, Title = "Digital Processing", IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 4, Title = "Informations Systems", IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 5, Title = "Database", IsActive = false });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 6, Title = "Web and Mobile", IsActive = true });

            context.SaveChanges();
        }
    }
}


