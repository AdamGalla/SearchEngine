using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//var client = new RestClient("http://localhost:9000/");
//var request = new RestRequest("LoadBalancer/RegicterService/" + Environment.MachineName, Method.Post);
//var queryResult = client.Execute(request);
//Console.Write(queryResult.StatusCode);
Console.WriteLine("Hostname: " + Environment.MachineName);
var client = new HttpClient();
var requestUrl = "http://loadbalancer/LoadBalancer/RegisterService";

var requestData = new { Url = Environment.MachineName };
var requestBody = new StringContent(JsonConvert.SerializeObject(requestData));
requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

var response = await client.PostAsync(requestUrl, requestBody);

Console.WriteLine(response.StatusCode.ToString());
var configuration = builder.Configuration;
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders()
           .AddDebug()
           .AddConsole()
           .AddConfiguration(configuration.GetSection("Logging"))
           .SetMinimumLevel(LogLevel.Information);
});

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
