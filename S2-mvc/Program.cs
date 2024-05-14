using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DaL;
using DataLogicLayer.DaL;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddScoped<IBlogRepository, BlogRepository>();
//builder.Services.AddScoped<BlogService>();

//builder.Services.AddScoped<ICategoryRepository, CategorieRepository>();
//builder.Services.AddScoped<CategorieService>();

//builder.Services.AddScoped<ILoginRepository, LoginRepository>();
//builder.Services.AddScoped<LoginService>();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
