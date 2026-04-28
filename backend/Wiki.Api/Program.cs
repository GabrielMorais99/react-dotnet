using Microsoft.OpenApi;
using Wiki.Api.Features.V1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Wiki API",
            Version = "v1",
        });
});

var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
             ?? ["http://localhost:5173"];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
        options.DisplayRequestDuration();
    });
}

app.UseCors();
app.UseHttpsRedirection();

app.MapSystemEndpointsV1();

app.Run();
