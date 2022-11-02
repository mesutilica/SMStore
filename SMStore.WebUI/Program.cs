using FluentValidation; // FluentValidation � kullanabilmek i�in
using SMStore.Data;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.Service.ValidationRules;
using SMStore.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies; // Authentication k�t�phanesini projeye ekliyoruz

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DatabaseContext>(); // DbContext i ekliyoruz

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); // Kendi yazd���m�z repository servisini burada uygulamaya ekliyoruz. Burada eklemeden projede kullanmaya kalkarsak hata al�r�z!!

// .Net Core ile birlikte 3 farkl� Dependency Injection y�ntemi var say�lan olarak kullan�m�m�za sunulmu�tur
// Dependency Injection Y�ntemleri :
// 1-AddSingleton : Bu y�ntemi kullan�rsak olu�turmak istedi�imiz nesneden 1 tane olu�turulur ve her iste�imizde bu nesne bize g�nderilir
// 2-AddTransient : Olu�turulmas� istenen nesneden her istek i�in yeni 1 tane olu�turulur
// 3-AddScoped : Olu�turulmas� istenen nesne i�in gelen iste�e bak�larak nesne daha �nceden olu�turulmu�sa onu olu�turulmam��sa yeni bir tane olu�turup onu g�nderir.

builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // Uygulamaya ICategoryRepository i kullanmak i�in istek yap�l�rsa CategoryRepository nesnesinden bir �rne�i kullan�lmak �zere g�nder

// FluentValidation ile class � kontrol etmek i�in
builder.Services.AddScoped<IValidator<AppUser>, AppUserValidator>();
builder.Services.AddScoped<IValidator<AdminLoginViewModel>, AdminLoginViewModelValidator>();
// yukar�daki FluentValidation servislerini ekledikten sonra bu servisi kullanaca��m�z controller da servisi kullanarak validasyon yapabiliriz

// Admin login oturum a�ma ayarlar� :
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>
{
    x.LoginPath = "/Admin/Login"; // admin paneline girmek isteyen yetkisiz kullan�c�lar� y�nlendirir
    x.AccessDeniedPath = "/AccessDenied"; // yetki kontrol� yaparsak yetkisi olmayanlar� bu sayfaya y�nlendirir
    x.LogoutPath = "/Admin/Logout";
    x.Cookie.Name = "Admin"; // Olu�acak cookie ye Admin ismini verdirdik
    x.Cookie.MaxAge = TimeSpan.FromDays(3); // Olu�acak cookie nin ya�am s�resi
    x.Cookie.IsEssential = true;
});

// Admin login yetkilendirme ayarlar� :
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // Admin yetkisi, bu yetkiye g�re kontrol yapacaksak admin controller larda authorize attribute �nde bu yetkiyi eklemeliyiz
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

app.UseAuthentication(); // Oturum a�ma
app.UseAuthorization(); // Oturum a�an kullan�c�y� yetkilendirme

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
