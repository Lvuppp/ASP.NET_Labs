using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets_Lab1.Models;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;

var builder = WebApplication.CreateBuilder(args);

UriData.ISUri = builder.Configuration.GetSection("UriData")["ISUri"];
UriData.ApiUri = builder.Configuration.GetSection("UriData")["ApiUri"];
// Add services to the container.GetUriData
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<IProductService, ApiProductService>(opt =>opt.BaseAddress=new Uri(UriData.ApiUri));
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt =>opt.BaseAddress=new Uri(UriData.ApiUri));
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});


app.Run();