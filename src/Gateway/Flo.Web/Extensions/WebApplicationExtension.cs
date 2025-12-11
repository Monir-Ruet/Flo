using Flo.Web.Endpoints.main;

namespace Flo.Web.Extensions;

public static class WebApplicationExtension
{
    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        app.UseHttpsRedirection();

        var routeGroup = app
            .MapGroup("");

        routeGroup.MapIdentityEndpoints();
        return app;
    }
}