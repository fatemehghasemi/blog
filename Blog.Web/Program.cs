using Blog.Application;
using Blog.Infrastructure;
using Blog.Web.Endpoints;
using blog.Web.Components;
  
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseSwagger();
app.UseSwaggerUI();

app.MapStaticAssets();
app.MapArticlesEndpoints();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();

