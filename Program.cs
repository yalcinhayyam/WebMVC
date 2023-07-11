using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

XmlConfigurator.Configure(new FileInfo("log4net.config"));

{
    var services = builder.Services;
    
    services.AddLogging(config =>
    {
        config.ClearProviders();
        config.AddLog4Net();
    });

    services.AddControllersWithViews();
    services.AddHttpClient("BaseClient", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]!);
    });
}



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

