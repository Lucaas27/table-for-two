using TableForTwo.Worker;
using TableForTwo.Worker.Infrastructure.Configuration;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddApplicationOptions(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
