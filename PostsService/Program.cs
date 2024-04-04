using PostsService.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreServices(builder.Configuration);

//Add Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

//Init database
//app.Services.GetRequiredService<AppDbContext>().Database.Migrate();

// Configure the HTTP request pipeline.
app.UseCoreApp();
app.Run();
