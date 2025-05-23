using Frontend.Client.Pages;

using Frontend.Components;
using MudBlazor.Services;
using MudBlazor;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBlazorBootstrap();
var BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:4000");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(BaseAddress.ToString()) });
builder.Services.AddHttpClient();
// builder.Services.AddDbContextPool<AppDBContext>(options =>
// {
//     var connetionString = "server=127.0.0.1;uid=root;pwd=rsbr220Sql!;database=paleo"; //Configuration.GetConnectionString("DefaultConnection");
//     options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
//     
//
// });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Frontend.Client._Imports).Assembly);

app.Run();