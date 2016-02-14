namespace OptionsWebSite.Migrations.OptionsMigrations
{
    using DiplomaDataModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.Models.OptionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\OptionsMigrations";
        }

        protected override void Seed(DiplomaDataModel.Models.OptionsContext context)
        {
            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 1, Year = 2015, Term = 20, IsDefault = false });
            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 2, Year = 2015, Term = 30, IsDefault = false });
            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 3, Year = 2016, Term = 10, IsDefault = false });
            context.YearTerms.AddOrUpdate(y => y.YearTermId, new YearTerm { YearTermId = 4, Year = 2016, Term = 30, IsDefault = true });

            context.SaveChanges();

            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 1, Title = "Data Communications",  IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 2, Title = "Client Server",        IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 3, Title = "Digital Processing",   IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 4, Title = "Informations Systems", IsActive = true });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 5, Title = "Database",             IsActive = false });
            context.Options.AddOrUpdate(o => o.OptionId, new Option { OptionId = 6, Title = "Web and Mobile",       IsActive = true });

            context.SaveChanges();

        }
    }
}
