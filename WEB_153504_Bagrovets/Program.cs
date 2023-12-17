using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;
using Web_153504_Bagrovets_Lab1.Services;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;
using Web_153504_Bagrovets_Lab1.TagHelpers;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using Web_153504_Bagrovets_Lab1;

var builder = WebApplication.CreateBuilder(args);

UriData.ISUri = builder.Configuration.GetSection("UriData")["ISUri"];
UriData.ApiUri = builder.Configuration.GetSection("UriData")["ApiUri"];
// Add services to the container.GetUriData
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = "cookie";
        opt.DefaultChallengeScheme = "oidc";
    }).AddCookie("cookie").AddOpenIdConnect("oidc", options =>
    {
        options.Authority =
            builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
        options.ClientId =
            builder.Configuration["InteractiveServiceSettings:ClientId"];
        options.ClientSecret =
            builder.Configuration["InteractiveServiceSettings:ClientSecret"];

        options.GetClaimsFromUserInfoEndpoint = true;
        options.ResponseType = "code";
        options.ResponseMode = "query";
        options.SaveTokens = true;
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IProductService, ApiProductService>(opt =>opt.BaseAddress=new Uri(UriData.ApiUri));
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt =>opt.BaseAddress=new Uri(UriData.ApiUri));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PagerTagHelper>();

builder.Services.AddScoped<Cart>(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Filter.ByIncludingOnly(evt =>
    {
        if (evt.Properties.TryGetValue("StatusCode", out var statusCodeValue) &&
            statusCodeValue is ScalarValue statusCodeScalar &&
            statusCodeScalar.Value is int statusCode)
        {
            Debug.WriteLine("Report error with code 1xx, 3xx, 4xx ,5xx");
            return statusCode < 200 || statusCode >= 300;
        }
        return false;
    }));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages().RequireAuthorization();
app.UseSession(); 
app.UseLoggingMiddleware();
app.Run();