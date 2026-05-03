using Blog.Application;
using Blog.Infrastructure;
using Blog.Web.Endpoints;
  
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapArticlesEndpoints();
app.MapCommentsEndpoints();
app.MapLikesEndpoints();

app.Run();

