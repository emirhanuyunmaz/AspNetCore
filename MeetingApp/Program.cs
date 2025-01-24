var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddControllersWithViews(); //MVC yapısı için gerekli
app.MapGet("/", () => "Hello World!");

app.Run();
