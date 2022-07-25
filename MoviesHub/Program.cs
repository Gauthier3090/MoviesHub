using MoviesWorld_BLL.Services;
using Tools.Connections;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MoviesWorld_DAL.Interfaces;
using MoviesWorld_DAL.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient( _ =>
{
    Connection connection = new(
        SqlClientFactory.Instance,
        builder.Configuration.GetConnectionString("Default")
    );
    return connection;
});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IRepositoryUser, RepositoryUser>();
builder.Services.AddTransient<IRepositoryPublication, RepositoryPublication>();
builder.Services.AddTransient<IRepositoryComment, RepositoryComment>();
builder.Services.AddTransient<IRepositoryFollow, RepositoryFollow>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<PublicationService>();
builder.Services.AddTransient<CommentService>();
builder.Services.AddTransient<FollowService>();

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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
