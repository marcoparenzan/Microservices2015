using Call4Pizza.Models;
using Call4Pizza.Models.Contracts;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Http.Client
{
    public class TotemHttpClient: ITotem
    {
        private HttpClient _httpClient;
        private string _baseUri;

        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Redirect URI is the URI where Azure AD will return OAuth responses.
        // The Authority is the sign-in URL of the tenant.
        //
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static Uri redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);

        private static string authority = ""; // String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        private AuthenticationContext _authContext;

        public TotemHttpClient(): this(ConfigurationManager.AppSettings["TotemHttpClient-BaseUri"])
        {
            _authContext = null; //  new AuthenticationContext(authority, new FileCache());
        }

        public TotemHttpClient(string baseUri)
        {
            _baseUri = baseUri;
            _httpClient = new HttpClient();
        }

        async Task<ITotem> ITotem.NewPizzasToPrepareAsync(Guid commandId)
        {
            var response = await GetAsync(_baseUri + "NewPizzasToPrepare" + "?commandId=" + commandId.ToString());
            // var response = await ExecuteAsync(new { commandId = commandId }, "NewPizzasToPrepare");
            return this;
        }

        protected async Task<HttpResponseMessage> ExecuteAsync<TCommand>(TCommand command, string path)
        {
            var content = new MultipartFormDataContent();
            var commandJson = JsonConvert.SerializeObject(command);
            var stringContent = new StringContent(commandJson);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Add(stringContent, "command");

            var response = await PostAsync(
                _baseUri + path
                , stringContent
            );

            return response;
        }

        protected async Task<HttpResponseMessage> PostAsync(string path, HttpContent content, PromptBehavior promptBehavior = PromptBehavior.Auto)
        {
            //
            // Get an access token to call the To Do service.
            //
            AuthenticationResult result = null;
            try
            {
//                result = this._authContext.AcquireToken(ConfigurationManager.AppSettings["ida:Audience"], clientId, redirectUri, promptBehavior);
            }
            catch (AdalException ex)
            {
                // There is no access token in the cache, so prompt the user to sign-in.
                if (ex.ErrorCode == "user_interaction_required")
                {
                    throw new UnauthorizedAccessException("Please login first");
                }
                else
                {
                    // An unexpected error occurred.
                    throw ex;
                }
            }

            // Once the token has been returned by ADAL, add it to the http authorization header, before making the call to access the To Do list service.
  //          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            // Call the To Do list service.
            HttpResponseMessage response = await _httpClient.PostAsync(path, content);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }


        protected async Task<HttpResponseMessage> GetAsync(string path)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }
    }
}
