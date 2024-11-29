using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// EZ-Shop Configuration:
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
builder.Configuration.GetSection(nameof(AppSettings)).Bind(new AppSettings());
builder.Services.AddScoped<IService<Category, CategoryModel>, CategoryService>();
builder.Services.AddScoped<IService<Store, StoreModel>, StoreService>();
builder.Services.AddScoped<IService<Country, CountryModel>, CountryService>();
builder.Services.AddScoped<IService<City, CityModel>, CityService>();
builder.Services.AddScoped<IService<Product, ProductModel>, ProductService>();
builder.Services.AddScoped<IService<User, UserModel>, UserService>();
builder.Services.AddScoped<IService<Role, RoleModel>, RoleService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Users/Login";
        config.AccessDeniedPath = "/Users/Login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });
builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30);
    config.IOTimeout = Timeout.InfiniteTimeSpan;
});

var app = builder.Build();

// EZ-Shop Configuration:
app.UseAuthentication();
app.UseSession();

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
