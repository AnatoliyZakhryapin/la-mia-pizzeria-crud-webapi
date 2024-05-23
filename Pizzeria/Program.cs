using Pizzeria.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Pizzeria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PizzaManager.PizzaSeeder();

            var builder = WebApplication.CreateBuilder(args);
             //var connectionString = builder.Configuration.GetConnectionString("PizzeriaDatabaseContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzeriaDatabaseContextConnection' not found.");
            //var connectionString = builder.Configuration.GetConnectionString("PizzeriaDatabaseContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzeriaDatabaseContextConnection' not found.");
            //var connectionString = builder.Configuration.GetConnectionString("PizzeriaDatabaseContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzeriaDatabaseContextConnection' not found.");

            //builder.Services.AddDbContext<PizzeriaDatabaseContext>(options =>options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<PizzeriaDatabaseContext>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PizzeriaDatabaseContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}