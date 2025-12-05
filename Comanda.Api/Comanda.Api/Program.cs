using Comanda.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ComandasDBContext>(options =>
    options.UseSqlite("DataSource=ComandasDb")
    );
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPolitica", policy =>
    {
        policy.WithOrigins("http://localhost", "http://127.0.0.1:5500", "http://127.0.0.1:5501", "http://127.0.0.1") // Origens permitidas
        .AllowAnyHeader() // Permite qualquer cabeçalho
        .AllowAnyMethod(); // Permite qualquer método HTTP
    });
});

var app = builder.Build();

//criar o banco de dados

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ComandasDBContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors("MinhaPolitica");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("MinhaPolitica");

app.MapControllers();

app.Run();
