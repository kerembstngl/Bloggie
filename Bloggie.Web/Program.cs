using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddControllersWithViews();

// Dbcontexti builder �zelliklerini kullanarak �a��ral�m
builder.Services.AddDbContext<BloggieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));

// Olu�turmu� oldu�umuz tag repositoryi ve itaginterface i art�k dbcontextmi� gibi kullanabilmek ve classlar�m�za enjekte edebilmek i�in a�a��daki builder servisini kullan�yoruz. Art�k controllerda tagrepositoryi kullanarak DbContex eri�imini dolayl� yoldan sa�lam�� olaca��z.

builder.Services.AddScoped<ITagInterface, TagRepository>();

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
