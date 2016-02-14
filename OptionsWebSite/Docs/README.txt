Enable-Migrations -ContextProjectName DiplomaDataModel -ContextTypeName OptionsContext -MigrationsDirectory Migrations\OptionsMigrations

add-migration -ConfigurationTypeName OptionsWebSite.Migrations.OptionsMigrations.Configuration "Initial Create"

update-database -ConfigurationTypeName OptionsWebSite.Migrations.OptionsMigrations.Configuration



ROLLBACK ALL MIGRATIONS
-----------------------
Update-Database -TargetMigration:0

ROLLBACK TO A MIGRATION
-----------------------
Update-Database -TargetMigration:"MigrationName"




Just DO NOT DELETE TABLES... :(

IN CASE OF EMERGENCY ( i.e. you delete the migration history )
--------------------------------------------------------------
Enable-Migrations -ContextProjectName DiplomaDataModel -EnableAutomaticMigrations -Force
Add-Migration Initial -Force
Update-Database
//Recreate what was in there
//Force quit Visual Studio
//Reopen