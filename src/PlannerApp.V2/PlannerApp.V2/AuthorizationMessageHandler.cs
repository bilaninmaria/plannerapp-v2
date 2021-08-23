using Blazored.LocalStorage;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PlannerApp.V2
{
    public partial class Program
    {
        //This method will retrieve the access token from the local storage, in case it is there and set it in the header.
        public class AuthorizationMessageHandler : DelegatingHandler
        {
            //Inject the service .
            private readonly ILocalStorageService _storage;

            public AuthorizationMessageHandler(ILocalStorageService storage)
            {
                _storage = storage;
            }
            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                //Check if there is any access token in local storage.
                if (await _storage.ContainKeyAsync("access_token"))
                {
                    //In case is there , will bring it.
                    var token = await _storage.GetItemAsStringAsync("access_token");
                    //Put it in the header.
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                Console.WriteLine("Authorization Message Handler Called.");
                //Return the response back.
                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}