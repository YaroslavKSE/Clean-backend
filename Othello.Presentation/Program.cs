using Othello.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblyContaining<Othello.Application.UseCases.LoginUserCommand>());
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblyContaining<Othello.Presentation.Controllers.AuthController>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
