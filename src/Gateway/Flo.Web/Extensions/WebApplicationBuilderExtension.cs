using System.Reflection;
using Flo.Core.Shared.Constants;
using Flo.Proto;
using Flo.ServiceDefaults;
using FluentValidation;

namespace Flo.Web.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();

        builder.AddServiceDefaults();

        builder.AddDefaultAuthentication();

        builder.Services.AddDaprClient();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        builder.AddDaprGrpcClient<Auth.AuthClient>(ServiceInvocations.MainService);

        return builder;
    }
}