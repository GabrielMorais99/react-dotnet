using Wiki.Api.Features.V1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();

app.MapSystemEndpointsV1();

app.Run();
