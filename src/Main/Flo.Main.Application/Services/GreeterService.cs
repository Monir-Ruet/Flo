using Flo.Proto;
using Grpc.Core;

namespace Flo.Main.Application.Services;

public class GreeterService : Auth.AuthBase
{
    public override Task<AccessTokenResponse> Login(LoginRequest request, ServerCallContext context)
    {
        return Task.FromResult(new AccessTokenResponse()
        {

        });
    }
}