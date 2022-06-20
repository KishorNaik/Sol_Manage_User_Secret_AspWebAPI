//https://www.infoworld.com/article/3576292/how-to-work-with-user-secrets-in-asp-net-core.html
//https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows

using Sol_Demo.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get ConnectionString from the AppSetting File.
var conStrBuilder = new SqlConnectionStringBuilder(builder?.Configuration?.GetConnectionString("CustomerDB"));

// Get UserId and Password from the User secret.json
SecretModel? secretModel = builder?.Configuration?.GetSection("DB")?.Get<SecretModel>();

// Append UserID and Password in the Connection String
conStrBuilder.UserID = secretModel.UserID;
conStrBuilder.Password = secretModel.Password;

// Get Final Connectionstring
var connection = conStrBuilder.ConnectionString;
Console.WriteLine($"Connection String {connection}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();