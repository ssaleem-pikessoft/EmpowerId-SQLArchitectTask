using EmpowerId.Persistence;
using EmpowerId.Persistence.Data;
using EmpowerId.MediatR.Extension;
using EmpowerId.Persistence.Extension;
using EmpowerId.Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    //var db = scope.ServiceProvider.GetRequiredService<EmpowerDbContext>();
    //if (db.Database.GetPendingMigrations().Any())
    //{
    //    db.Database.Migrate();
    //}
}

app.Run();
