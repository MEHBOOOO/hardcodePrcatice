using Microsoft.EntityFrameworkCore;
using OrderService.Data; 
using OrderService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
