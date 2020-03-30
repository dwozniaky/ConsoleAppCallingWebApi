using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace ConsoleAppCallingWebApi
{
    class Program
    {
        private const string ClientId = ""; // Desktop App ClientId
        private const string ClientSecret = ""; // Desktop App ClientSecret
        private const string Authority = "https://login.microsoftonline.com/" + "Tenant" + "/"; // for single tenant 
        private const string ApiScope = "resource/.default"; // scope is ALWAYS {resource}/.default

        static void Main(string[] args)
        {
            try
            {
                RunAsyncTask().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static async Task RunAsyncTask()
        {
            // Even if this is a console application here, a daemon application is a confidential client application therefore it is required to use 'ConfidentialClientApplicationBuilder'
            // In this sample we will be using the Console Application based on the Client Secret Credential Flow 

            var app = ConfidentialClientApplicationBuilder.Create(ClientId)
                .WithClientSecret(ClientSecret)
                .WithAuthority(new Uri(Authority))
                .Build();


            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator
            var scopes = new[] { ApiScope };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes)
                    .ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired \n");
                Console.ResetColor();
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Scope provided is not supported");
                Console.ResetColor();
            }

            if (result != null)
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                var response = await httpClient.GetAsync("https://localhost:44321/api/home");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(content);
                    Console.ResetColor();
                }
            }
        }
    }
}
