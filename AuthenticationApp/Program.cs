using AuthenticationApp.Context;
using AuthenticationApp.Models;
using AuthenticationApp.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
        builder.Services.AddIdentity<User, IdentityRole>(opts => {
            opts.Password.RequiredLength = 1;   // минимальная длина
            opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
            opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
            opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
            opts.Password.RequireDigit = false; // требуются ли цифры
            opts.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationContext>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();//

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
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

        app.Run();
    }
}