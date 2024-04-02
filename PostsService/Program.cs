using PostsService.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCoreApp();
app.Run();
