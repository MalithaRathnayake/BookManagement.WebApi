using BookManagement.Application.Interfaces;
using BookManagement.Application.Services;
using BookManagement.Domain.Interfaces;
using BookManagement.Infrastructure.Data;
using BookManagement.Infrastructure.Repositories; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Configure Entity Framework Core with In-Memory Database
builder.Services.AddDbContext<BookContext>(options =>
    options.UseInMemoryDatabase("BookDb"));

// Register BookRepository with DI
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
