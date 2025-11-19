using BankSystemWebApi.Data;
using BankSystemWebApi.Models;
using BankSystemWebApi.Services;
using BankSystemWebApi.Services.Interfaces;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:7000");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApplicationConfig>(builder.Configuration.GetSection("ApplicationConfigs"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ApplicationConfig>>().Value);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
        RequestPath = new PathString("/Uploads"),
        ServeUnknownFileTypes = true,
        OnPrepareResponse = ctx =>
            ctx.Context.Response.Headers.Append(
                "Cache-Control", $"public, max-age={604800}")
    });



app.Run();

