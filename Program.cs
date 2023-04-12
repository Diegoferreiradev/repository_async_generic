using Microsoft.EntityFrameworkCore;
using ProdutosApi.DbContexts;
using ProdutosApi.Repositories;
using ProdutosApi.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

var conexaoDB = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseSqlServer(conexaoDB);
});

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();


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

app.Run();
