using Microsoft.EntityFrameworkCore;
using ShopCenter.Data.Context;
using ShopCenter.IoC.Dependency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DB Context

builder.Services.AddDbContext<ShopCenterDBContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ShopCenterDBConnection"));
    }
    );

#endregion


DependencyContainer.RegisterServices(builder.Services);

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
    name: "areas",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
