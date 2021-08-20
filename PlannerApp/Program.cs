using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddHttpClient("PlannerApp.Api", client =>
            {
                client.BaseAddress = new Uri("https://plannerapp-api.azurewebsites.net");
            }).AddHttpMessageHandler<AuthorizationMessageHandler>();

            builder.Services.AddTransient<AuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("PlannerApp.Api"));
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddMudServices();


            await builder.Build().RunAsync();
        }
    
    
    
    
    
    
    }




}

//User Registered with
//https://plannerapp-api.azurewebsites.net/api/v2/Auth/Register
//{
//  "email": "adnan.umar@bayyinahSol.com",
//  "firstName": "adnan",
//  "lastName": "umar",
//  "password": "Adnan.123",
//  "confirmPassword": "Adnan.123"
//}