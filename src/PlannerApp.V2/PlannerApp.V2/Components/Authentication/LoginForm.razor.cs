using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlannerApp.V2.Components
{
    public partial class LoginForm : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        //Tell the application that the user is logged in.
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        //Store the access token after the response comes back from the server.
        [Inject]
        public ILocalStorageService Storage { get; set; }
        private LoginRequest _model = new();

        //To show some reaction from the user(to avoid hyper clicking).
        private bool _IsBusy = false;
        private string _errorMessage = string.Empty;

        private async Task LoginUserAsync()
        {
            _IsBusy = true;
            _errorMessage = string.Empty;

            var response = await HttpClient.PostAsJsonAsync("/api/v2/auth/login", _model);
            if (response.IsSuccessStatusCode)
            {
                //There we have the token.
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>();
                //Store it in local storage.
                await Storage.SetItemAsStringAsync("access_token", result.Value.Token);
                //Store the expiry date .
                await Storage.SetItemAsync<DateTime>("expiry_date", result.Value.ExpiryDate);

                //Populate the user object and notify the application that the Authentication state is changed.
                await AuthenticationStateProvider.GetAuthenticationStateAsync();

                Navigation.NavigateTo("/");
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                _errorMessage = errorResult.Message;
            }
            _IsBusy = false;

        }
    }
}