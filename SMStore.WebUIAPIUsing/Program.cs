using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using SMStore.Data;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.Service.ValidationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddHttpClient(); // Api mize istek gönderebilmek için gerekli servis!!

builder.Services.AddDbContext<DatabaseContext>(); // DbContext i ekliyoruz

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));

builder.Services.AddScoped<IValidator<AppUser>, AppUserValidator>();
//builder.Services.AddScoped<IValidator<AdminLoginViewModel>, AdminLoginViewModelValidator>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // admin paneline girmek isteyen yetkisiz kullanýcýlarý yönlendirir
    x.AccessDeniedPath = "/AccessDenied"; // yetki kontrolü yaparsak yetkisi olmayanlarý bu sayfaya yönlendirir
    x.LogoutPath = "/Admin/Logout";
    x.Cookie.Name = "Admin"; // Oluþacak cookie ye Admin ismini verdirdik
    x.Cookie.MaxAge = TimeSpan.FromDays(3); // Oluþacak cookie nin yaþam süresi
    x.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // Admin yetkisi, bu yetkiye göre kontrol yapacaksak admin controller larda authorize attribute ünde bu yetkiyi eklemeliyiz
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User"));
});

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

app.UseAuthentication(); // Oturum açma
app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
