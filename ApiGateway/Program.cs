using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// Add Reverse Proxy
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();
