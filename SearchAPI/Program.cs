using SearchAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? loadBalancerURL = builder.Configuration.GetSection("LoadBalancerUrl").Value;
if (loadBalancerURL is null)
{
    throw new ArgumentNullException(nameof(loadBalancerURL), "Failed to retrieve loadBalancer URL. Is it defined in appsettings.json?");
}

RegisterService.Register(loadBalancerURL, "");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

}

app.UseRouting();

app.MapControllers();

app.Run();
