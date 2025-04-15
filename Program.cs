using ContactManagement.DataAccess;
using ContactManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Get the database connection string
            var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

            // Add EF configuration
            builder.Services.AddDbContext<AppDbContext>(options
                => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute
            (
                name: "default",
                pattern: "{controller=Login}/{action=CreateAccount}/{id?}"
            ).WithStaticAssets();

            app.Run();
        }
    }
}
