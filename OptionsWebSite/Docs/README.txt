Enable-Migrations -ContextProjectName DiplomaDataModel -ContextTypeName OptionsContext -MigrationsDirectory Migrations\OptionsMigrations

add-migration -ConfigurationTypeName OptionsWebSite.Migrations.OptionsMigrations.Configuration "Initial Create"

update-database -ConfigurationTypeName OptionsWebSite.Migrations.OptionsMigrations.Configuration



ROLLBACK ALL MIGRATIONS
-----------------------
Update-Database -TargetMigration:0

ROLLBACK TO A MIGRATION
-----------------------
Update-Database -TargetMigration:"MigrationName"