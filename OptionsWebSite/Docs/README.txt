Enable-Migrations -ContextProjectName DiplomaDataModel -ContextTypeName OptionsContext -MigrationsDirectory Migrations\OptionsMigrations

add-migration -ConfigurationTypeName DiplomaDataModel.Models.Configuration "Initial Create"

update-database -ConfigurationTypeName DiplomaDataModel.Models.Configuration



ROLLBACK ALL MIGRATIONS
-----------------------
Update-Database -TargetMigration:0

ROLLBACK TO A MIGRATION
-----------------------
Update-Database -TargetMigration:"MigrationName"