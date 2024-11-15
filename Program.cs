using Microsoft.EntityFrameworkCore;
using TestFinal5.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
   options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=finshAsp;Integrated Security=True");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=index}/{id?}");

app.Run();
