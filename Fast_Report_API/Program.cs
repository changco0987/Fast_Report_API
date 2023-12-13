using FastReport.Data;
using FastReport.Utils;
using FastReport;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;
using Fast_Report_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injection
builder.Services.AddDbContext<PghContext>(item => item.UseMySQL(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.Data.js));
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseFastReport();

app.Run();
