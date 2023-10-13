using EduMapBackendProject;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.Register(config);

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=exists}/{controller=AdminHome}/{action=Dashboard}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
