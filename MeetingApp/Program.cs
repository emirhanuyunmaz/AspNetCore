var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(); //MVC yapısı için gerekli
var app = builder.Build();

// {controller=Home}/{action=Index}/id?
// app.MapDefaultControllerRoute(); //Kısa olarak tanımlanırsa

app.UseStaticFiles(); //Static dosyaların kullanımı için gerekli.

app.UseRouting(); //Middleware sırası ve düzeni belirlenmesinde kullanılıyor.

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}"
);

app.Run();
