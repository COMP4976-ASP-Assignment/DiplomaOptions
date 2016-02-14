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
            List<Option> options = new List<Option>() {
               new Option {Title="Data Communications",  IsActive=true},
               new Option {Title="Client Server",        IsActive=true},
               new Option {Title="Digital Processing",   IsActive=true},
               new Option {Title="Informations Systems", IsActive=true},
               new Option {Title="Database",             IsActive=false},
               new Option {Title="Web and Mobile",       IsActive=true},
             };

            context.Options.AddOrUpdate(o => o.OptionId, options.ToArray());

        }
    }
}
