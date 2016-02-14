Enable-Migrations -ContextTypeName CoursesContext -MigrationsDirectory Migrations\CoursesMigrations

add-migration -ConfigurationTypeName Courses.Web.Migrations.CourseMigrations.Configuration "InitialCreate"

update-database -ConfigurationTypeName Courses.Web.Migrations.CourseMigrations.Configuration