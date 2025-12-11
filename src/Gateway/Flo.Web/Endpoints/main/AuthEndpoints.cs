using AutoMapper;
using Flo.Proto;
using Flo.Web.DTOs.Identity;
using Flo.Web.Filters;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace Flo.Web.Endpoints.main;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var route = app.MapGroup("/auth").WithTags("Identity");

        route.MapPost("/login", Login)
            .AddEndpointFilter<ValidationFilter<LoginRequestDto>>()
            .Produces<AccessTokenResponse>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }
    
    private static async Task<IResult> Login(
        [FromBody] LoginRequestDto loginRequest,
        Auth.AuthClient client,
        IMapper mapper)
    {
        try
        {
            var login = mapper.Map<LoginRequest>(loginRequest);
            var loginResult = await client.LoginAsync(login);
            return Results.Ok(loginResult);
        }
        catch (RpcException ex)
        {
            return Results.BadRequest(ex);
        }
    }
}