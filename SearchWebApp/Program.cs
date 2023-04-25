using FeatureHubSDK;
using SearchWebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddSingleton<IApiClient>(apiClient => new ApiClient());
var featureHubCfg = new EdgeFeatureHubConfig("http://localhost:8085", "3996fba9-d379-4fa2-bbd4-0d027cf0c694/WZJlb0y374aZelOnCLtKtw76OFEErv38GRLUAnmO");
builder.Services.AddSingleton<IFeatureHubConfig>(featureHubConfig => featureHubCfg);

builder.Services.AddSingleton(featureHubContext => featureHubCfg.NewContext().Build().GetAwaiter().GetResult());
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
