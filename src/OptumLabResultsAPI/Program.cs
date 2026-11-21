using Microsoft.EntityFrameworkCore;
using OptumLabResultsAPI.Data;
using OptumLabResultsAPI.Middleware;
using OptumLabResultsAPI.Repositories;
using OptumLabResultsAPI.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////For In-Memory resitory use these 
//builder.Services.AddSingleton<ILabResultRepository, InMemoryLabResultRepository>();
//builder.Services.AddScoped<ILabResultService, LabResultService>();

//EF CORE -SQL Connection
builder.Services.AddDbContext<LabResultDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILabResultRepository, SqlLabResultRepository>();
builder.Services.AddScoped<ILabResultService, LabResultService>();

var app = builder.Build();


app.MapGet("/", () => "Welcome to Optum Lab Results API!");

app.UseMiddleware<ExceptionHandlingMiddleware>();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }
