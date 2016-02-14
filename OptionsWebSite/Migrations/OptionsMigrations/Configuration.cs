namespace OptionsWebSite.Migrations.OptionsMigrations
{
    using DiplomaDataModel.Models;
    using System;
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
            /*
            context.Options.AddOrUpdate(
              o => o.OptionId,
              new Option {
                  Title    = "Data Communications",
                  IsActive = true
              },
              new Option {
                  Title    = "Client Server",
                  IsActive = true
              },
              new Option {
                  Title    = "Digital Processing",
                  IsActive = true
              },
              new Option
              {
                  Title    = "Information Systems",
                  IsActive = true
              },
              new Option
              {
                  Title    = "Database",
                  IsActive = false
              },
              new Option
              {
                  Title    = "Web & Mobile",
                  IsActive = true
              },
              new Option
              {
                  Title    = "Tech Pro",
                  IsActive = false
              }
            );
            */


            context.SaveChanges();

        }
    }
}
