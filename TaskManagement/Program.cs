using TaskManagement.Data.AppData;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Interfaces;
using TaskManagement.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using TaskManagement.Services;

//Admin, 000000
//Moder, 123123
//от всех почти пароли 000000

//


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
  });

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICommentRepository, CommentsRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Инициализация бд
builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
{
  // Подключение к бд находится в appsettings.json
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add services to the container.
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

//Add Authorize + Authenticate
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
