using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.API.Data;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;

var builder = WebApplication.CreateBuilder(args);


// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("ConnectionStrings");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
builder.Services.AddScoped<IProductService, MemoryProductService>();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();