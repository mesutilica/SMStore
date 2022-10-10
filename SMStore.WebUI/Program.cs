using SMStore.Data;
using SMStore.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(); // DbContext i ekliyoruz

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); // Kendi yazd���m�z repository servisini burada uygulamaya ekliyoruz. Burada eklemeden projede kullanmaya kalkarsak hata al�r�z!!

// .Net Core ile birlikte 3 farkl� Dependency Injection y�ntemi var say�lan olarak kullan�m�m�za sunulmu�tur
// Dependency Injection Y�ntemleri :
// 1-AddSingleton : Bu y�ntemi kullan�rsak olu�turmak istedi�imiz nesneden 1 tane olu�turulur ve her iste�imizde bu nesne bize g�nderilir
// 2-AddTransient : Olu�turulmas� istenen nesneden her istek i�in yeni 1 tane olu�turulur
// 3-AddScoped : Olu�turulmas� istenen nesne i�in gelen iste�e bak�larak nesne daha �nceden olu�turulmu�sa onu olu�turulmam��sa yeni bir tane olu�turup onu g�nderir.

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
