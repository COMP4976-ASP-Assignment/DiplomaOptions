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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            context.YearTerms.AddOrUpdate(
                p => p.YearTermId,
                new YearTerm { Year = 2015 , Term = 20 , IsDefault = false},
                new YearTerm { Year = 2015, Term = 30, IsDefault = false },
                new YearTerm { Year = 2016, Term = 10, IsDefault = false },
                new YearTerm { Year = 2016, Term = 30, IsDefault = true }
             );
            context.SaveChanges();

            context.Options.AddOrUpdate(
             p => p.OptionId,
                 new Option { Title = "Data Communications" , IsActive = true},
                 new Option { Title = "Client Server", IsActive = true },
                 new Option { Title = "Digital Processing", IsActive = true },
                 new Option { Title = "Information Systems", IsActive = true },
                 new Option { Title = "Database", IsActive = false },
                 new Option { Title = "Web & Mobile", IsActive = true },
                 new Option { Title = "Tech Pro", IsActive = true }
             );
            context.SaveChanges();
        }
    }
}
