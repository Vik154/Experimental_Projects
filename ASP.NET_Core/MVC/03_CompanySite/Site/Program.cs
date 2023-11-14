using Site.Domain.Repositories.Abstract;
using Site.Domain.Repositories.EntityFramework;
using Site.Domain;
using Site.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Site;

public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Подключение конфига appsettings.json секции "Project"
        builder.Configuration.Bind("Project", new Config());

		// Подключаем нужный функционал приложения в качестве сервисов
		builder.Services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
		builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
		builder.Services.AddTransient<DataManager>();

		// Подключаем контекст БД
		builder.Services.AddDbContext<AppDbContext>((context) => context.UseSqlServer(Config.ConnectionString));

		// Настраиваем identity систему
		builder.Services.AddIdentity<IdentityUser, IdentityRole>((opts) => {
			opts.User.RequireUniqueEmail = true;
			opts.Password.RequiredLength = 6;
			opts.Password.RequireNonAlphanumeric = false;
			opts.Password.RequireLowercase = false;
			opts.Password.RequireUppercase = false;
			opts.Password.RequireDigit = false;
		}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

		// Настраиваем authentication cooke
		builder.Services.ConfigureApplicationCookie((options) => {
			options.Cookie.Name = "myCompanyAuth";
			options.Cookie.HttpOnly = true;
			options.LoginPath = "/account/login";
			options.AccessDeniedPath = "/account/accessdenied";
			options.SlidingExpiration = true;
		});

		// Настройка политики авторизации для Admin area
		builder.Services.AddAuthorization((x) => {
			x.AddPolicy("AdminArea", (policy) => { policy.RequireRole("admin"); });
		});

		// Добавляем поддержку контроллеров и представлений (MVC)
		builder.Services.AddControllersWithViews();
		//builder.Services.AddControllersWithViews((x) => {
		//	x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
		//})

		var app = builder.Build();

		// Порядок регистрации middleware очень важен
		// Подробная информация об ошибках в процессе разработки
		if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

		// Подключение поддержки статичных файлов в приложении (css, js)
		app.UseStaticFiles();

		// Подключение системы маршрутизации
		app.UseRouting();

		// Подключение аутентификации и авторизации
		app.UseCookiePolicy();
		app.UseAuthentication();
		app.UseAuthorization();

		// Регистрация нужных маршрутов
        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
		app.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
