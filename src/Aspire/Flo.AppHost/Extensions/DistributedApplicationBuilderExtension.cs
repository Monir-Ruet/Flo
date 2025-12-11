using CommunityToolkit.Aspire.Hosting.Dapr;

namespace Flo.AppHost.Extensions;

public static class DistributedApplicationBuilderExtension
{
    public static void ConfigureDistributedApplication(this IDistributedApplicationBuilder builder)
    {
        // var pubSub = builder.AddDaprPubSub("pubsub", new DaprComponentOptions()
        // {
        //     LocalPath = "../../../dapr/components/pubsub.rabbitmq.yaml"
        // });

        builder.AddProject<Projects.Flo_Web>("web")
            // .WithReference(pubSub)
            .WithDaprSidecar(new DaprSidecarOptions
            {
                AppId = "flo-web",
                AppProtocol = "grpc",
            });

        builder.AddProject<Projects.Flo_Main_Application>("main")
            .WithDaprSidecar(new DaprSidecarOptions
            {
                AppId = "flo-main",
                AppPort = 5047,
                AppProtocol = "grpc"
            });

        builder.AddProject<Projects.Flo_Messaging_Application>("messaging")
            // .WithReference(pubSub)
            .WithDaprSidecar(new DaprSidecarOptions
            {
                AppId = "flo-messaging",
                AppPort = 5246,
                AppProtocol = "grpc",
            });
    }
}