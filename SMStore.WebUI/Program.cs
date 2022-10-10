using SMStore.Data;
using SMStore.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(); // DbContext i ekliyoruz

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); // Kendi yazdýðýmýz repository servisini burada uygulamaya ekliyoruz. Burada eklemeden projede kullanmaya kalkarsak hata alýrýz!!

// .Net Core ile birlikte 3 farklý Dependency Injection yöntemi var sayýlan olarak kullanýmýmýza sunulmuþtur
// Dependency Injection Yöntemleri :
// 1-AddSingleton : Bu yöntemi kullanýrsak oluþturmak istediðimiz nesneden 1 tane oluþturulur ve her isteðimizde bu nesne bize gönderilir
// 2-AddTransient : Oluþturulmasý istenen nesneden her istek için yeni 1 tane oluþturulur
// 3-AddScoped : Oluþturulmasý istenen nesne için gelen isteðe bakýlarak nesne daha önceden oluþturulmuþsa onu oluþturulmamýþsa yeni bir tane oluþturup onu gönderir.

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

app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
