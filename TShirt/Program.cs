using Microsoft.EntityFrameworkCore;
using TShirt.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container - dependency injection to be done before the builder.Build()
builder.Services.AddControllersWithViews();

// .AddRazorRuntimeCompilation() only for post .NET 6
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//add connection strings
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
    //search in appsettings.json the matching value of the key inside the block of GetConnectionString
    builder.Configuration.GetConnectionString("TibzConnection")
    ));
//Singleton only create one instance, AddScoped will be with the lifetime of the request,  transient create a new object everytime (e.g each button click)
// Whenever we request an object of a category repository, it will give us the implementation defined in the category repository - changed to UnitOfWork for greater flexibility and not repeating DI for futur new Repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
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
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.Run();
 
