using ExpenseManager.Web;
using ExpenseManager.Web.Services;
using ExpenseManager.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
Console.WriteLine(builder.HostEnvironment.Environment);
foreach (var pair in builder.Configuration.AsEnumerable())
{
    Console.WriteLine($"{pair.Key} = {pair.Value}");
}

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClientFactory with API base URL from appsettings
builder.Services.AddHttpClient<ApiClient>((sp, client) =>
{
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5051";
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Register services
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();

await builder.Build().RunAsync();
