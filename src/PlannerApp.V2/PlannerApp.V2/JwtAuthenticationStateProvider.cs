using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlannerApp.V2
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;
        public JwtAuthenticationStateProvider(ILocalStorageService storage)
        {
            _storage = storage;

        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Check if there is any access token in local storage.
            if (await _storage.ContainKeyAsync("access_token"))
            {
                //The user is logged in
                //Read the token as a string.
                var tokenAsString = await _storage.GetItemAsStringAsync("access_token");
                //Create a token hanlder.
                var tokenHandler = new JwtSecurityTokenHandler();

                //Decode the token.
                var token = tokenHandler.ReadJwtToken(tokenAsString);

                //Take the claims and crate an identity object.
                var identity = new ClaimsIdentity(token.Claims, "Bearer");

                //User object which is a claim the principle just takes identity.
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);

                
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
                return authState;
            }

            //Empty claims principal means no identity and the user is not logged in.
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}