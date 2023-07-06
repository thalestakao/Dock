using Dock.Infra.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DockContext>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();


app.Run();
