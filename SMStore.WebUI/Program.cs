using FluentValidation; // FluentValidation ý kullanabilmek için
using SMStore.Data;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.Service.ValidationRules;
using SMStore.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies; // Authentication kütüphanesini projeye ekliyoruz

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DatabaseContext>(); // DbContext i ekliyoruz

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); // Kendi yazdýðýmýz repository servisini burada uygulamaya ekliyoruz. Burada eklemeden projede kullanmaya kalkarsak hata alýrýz!!

// .Net Core ile birlikte 3 farklý Dependency Injection yöntemi var sayýlan olarak kullanýmýmýza sunulmuþtur
// Dependency Injection Yöntemleri :
// 1-AddSingleton : Bu yöntemi kullanýrsak oluþturmak istediðimiz nesneden 1 tane oluþturulur ve her isteðimizde bu nesne bize gönderilir
// 2-AddTransient : Oluþturulmasý istenen nesneden her istek için yeni 1 tane oluþturulur
// 3-AddScoped : Oluþturulmasý istenen nesne için gelen isteðe bakýlarak nesne daha önceden oluþturulmuþsa onu oluþturulmamýþsa yeni bir tane oluþturup onu gönderir.

builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // Uygulamaya ICategoryRepository i kullanmak için istek yapýlýrsa CategoryRepository nesnesinden bir örneði kullanýlmak üzere gönder

// FluentValidation ile class ý kontrol etmek için
builder.Services.AddScoped<IValidator<AppUser>, AppUserValidator>();
builder.Services.AddScoped<IValidator<AdminLoginViewModel>, AdminLoginViewModelValidator>();
// yukarýdaki FluentValidation servislerini ekledikten sonra bu servisi kullanacaðýmýz controller da servisi kullanarak validasyon yapabiliriz

// Admin login oturum açma ayarlarý :
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>
{
    x.LoginPath = "/Admin/Login"; // admin paneline girmek isteyen yetkisiz kullanýcýlarý yönlendirir
    x.AccessDeniedPath = "/AccessDenied"; // yetki kontrolü yaparsak yetkisi olmayanlarý bu sayfaya yönlendirir
    x.LogoutPath = "/Admin/Logout";
    x.Cookie.Name = "Admin"; // Oluþacak cookie ye Admin ismini verdirdik
    x.Cookie.MaxAge = TimeSpan.FromDays(3); // Oluþacak cookie nin yaþam süresi
    x.Cookie.IsEssential = true;
});

// Admin login yetkilendirme ayarlarý :
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
app.UseAuthorization(); // Oturum açan kullanýcýyý yetkilendirme

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
