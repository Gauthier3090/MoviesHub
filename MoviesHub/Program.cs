using MoviesHub_BLL.Services;
using MoviesHub_DAL.Interfaces;
using MoviesHub_DAL.Repositories;
using Tools.Connections;
using System.Data.SqlClient;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient( _ =>
{
    Connection connection = new(
        SqlClientFactory.Instance,
        builder.Configuration.GetConnectionString("Default")
    );
    return connection;
});

builder.Services.AddTransient<IRepositoryUser, RepositoryUser>();
builder.Services.AddTransient<UserService>();

WebApplication app = builder.Build();

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
