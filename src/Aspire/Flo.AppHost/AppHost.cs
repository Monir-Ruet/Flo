using Flo.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);
builder.ConfigureDistributedApplication();
builder.Build().Run();