using System;
using System.Threading.Tasks;
using EstateApp.Data.DataBaseContexts.ApplicationDbContext;
using EstateApp.Data.DataBaseContexts.AuthenticationDbContext;
using EstateApp.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EstateApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AuthenticationDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("AuthenticationConnection"),
                // Set Migrations in EstateApp.Data
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("EstateApp.Data");
                }
            ));

            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("ApplicationConnection"),
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("EstateApp.Data");
                }
            ));

            // Register Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

            // Control User details
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Run DB Migrations when App starts
            MigrateDatabaseContexts(svp);

            CreateDefaultRolesAndUsers(svp).GetAwaiter().GetResult(); // Async function
        }

        public void MigrateDatabaseContexts(IServiceProvider svp)
        {
            var authenticationDbContext = svp.GetRequiredService<AuthenticationDbContext>();

            // Compare Migrations with Database and runs migrations
            authenticationDbContext.Database.Migrate();

            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }

        // Create Default Users and Default Roles
        public async Task CreateDefaultRolesAndUsers(IServiceProvider svp)
        {
            // Array of roles needed for the App
            string[] roles = new string[] { "SystemAdministrator", "Agent", "User" };

            // A user manager to manage things related 
            // To user in the app
            var userManager = svp.GetRequiredService<UserManager<ApplicationUser>>();
            var userEmail = "admin@estateapp.com";
            var userPassword = "SuperSecretPassword@2020";

            // Add User to a role
            var roleManager = svp.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    // Create the role
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            // Check if the User exist in the DB
            var user = await userManager.FindByEmailAsync("admin@estateapp.com");
            if (user is null)
            {
                user = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "+234123456789",
                    PhoneNumberConfirmed = true
                };

                await userManager.CreateAsync(user, userPassword);
                await userManager.AddToRolesAsync(user, roles);
            }
        }
    }
}
