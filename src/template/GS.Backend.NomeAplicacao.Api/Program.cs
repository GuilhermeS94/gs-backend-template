using GS.Backend.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Host.Init();
// Add services to the container.

builder.Services.Init(builder.Configuration);

var app = builder.Build();

app.Init();

app.Run();

