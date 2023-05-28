using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyProjectTodoApp;
using MyProjectTodoApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredToast();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BackEndApi"]) });
builder.Services.AddTransient<ITaskApiClient,TaskApiClient>();
builder.Services.AddTransient<IUserApiClient,UserApiClient>();

await builder.Build().RunAsync();



