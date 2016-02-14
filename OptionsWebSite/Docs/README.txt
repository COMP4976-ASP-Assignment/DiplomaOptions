enable-migrations -ContextProject DiplomaDataModel  -ContextTypeName DiplomaContext -MigrationsDirectory Migrations\DiplomaMigrations

add-migration -ConfigurationTypeName OptionsWebSite.Migrations.DiplomaMigrations.Configuration "InitialCreate"

update-database -ConfigurationTypeName OptionsWebSite.Migrations.DiplomaMigrations.Configuration



context.YearTerms.AddOrUpdate(
                p => p.YearTermId,
                new YearTerm { Year = 2015 , Term = 20 , IsDefault = false},
                new YearTerm { Year = 2015, Term = 30, IsDefault = false },
                new YearTerm { Year = 2016, Term = 10, IsDefault = false },
                new YearTerm { Year = 2016, Term = 30, IsDefault = true }
             );

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



            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var hasher = new PasswordHasher();
            var user = new ApplicationUser
            {
                UserName = "A00111111",
                PasswordHash = hasher.HashPassword("P@$$w0rd"),
                Email = "a@a.a",
                SecurityRole = "Admin"
                     
            };
           manager.Create(user, "A00111111");

            var user2 = new ApplicationUser
            {
                UserName = "A00222222",
                PasswordHash = hasher.HashPassword("P@$$w0rd"),
                Email = "s@s.s",
                SecurityRole = "Student"

            };
            manager.Create(user2, "A00222222");



			Regex for ID
			[RegularExpression(@"^(A00[0-9]{6,6})+$", ErrorMessage = "Invalid Id")]