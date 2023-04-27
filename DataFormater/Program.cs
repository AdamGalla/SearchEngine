using Common;
using DataFormatter.StrategyFactory;
using FeatureHubSDK;

var builder = WebApplication.CreateBuilder(args);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStrategyFactory>((factory) => new StrategyFactory());

var featureHubCfg = new EdgeFeatureHubConfig("http://localhost:8085", "7d3099f5-71b7-45ac-bc70-fb8c1779b2e0/3n4vSUHwfkq2lSS7h2uPZlh3Tr44Nk*Ln02Jpd38c78YJsVO0jx");

builder.Services.AddSingleton<IFeatureHubConfig>(featureHubConfig => featureHubCfg);
builder.Services.AddSingleton(featureHubContext => featureHubCfg.NewContext().Build().GetAwaiter().GetResult());
// Add services to the container.
builder.Services.AddControllers();
var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//app.UseMiddleware<TracingMiddleware>();

app.MapControllers();

app.Run();
