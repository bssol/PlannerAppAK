using Blazored.LocalStorage;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PlannerApp
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _storage;

        public AuthorizationMessageHandler(ILocalStorageService storage)
        {
            _storage = storage;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if(await _storage.ContainKeyAsync("access_token"))
            {
                var token = await _storage.GetItemAsStringAsync("access_token");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            Console.WriteLine("Http Handler called");

            return await base.SendAsync(request, cancellationToken);
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