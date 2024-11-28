using UserUniforAPI.Data;
using UserUniforAPI.Model;
using UserUniforAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddCors(options => { 
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.MapAPPEndpoint();
app.MapAPPEndpointProduct();
app.MapAPPEndpointPedido();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");


app.Run();

