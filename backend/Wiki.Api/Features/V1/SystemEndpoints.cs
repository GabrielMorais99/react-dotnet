namespace Wiki.Api.Features.V1;

/// <summary>Rotas exemplo — expanda em vertical slices (features) por pasta.</summary>
public static class SystemEndpoints
{
    public static IEndpointRouteBuilder MapSystemEndpointsV1(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1")
            .WithTags("system");

        group.MapGet("/hello", () =>
            Results.Ok(new HelloResponse(
                Message: "Wiki monorepo API",
                UtcNow: DateTime.UtcNow)));

        group.MapGet("/health", () => Results.Ok(new HealthResponse(Status: "ok")));

        return app;
    }

    private sealed record HelloResponse(string Message, DateTime UtcNow);

    private sealed record HealthResponse(string Status);
}
