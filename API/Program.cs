using API.Endpoints;
using Application.QCards;
using FluentValidation;
using MediatR;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MyDatabase>();
builder.Services.AddMediatR(typeof(Buy.Handler).Assembly);
builder.Services.AddScoped<IValidator<Buy.Command>, BuyValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api/card/").MapCardApi();

app.Run();