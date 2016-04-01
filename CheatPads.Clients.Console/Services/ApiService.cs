﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CheatPads.Clients.Console.Services
{

    public static class ApiService
    {

        public static JArray GetProducts()
        {
            return HttpGetArray(Settings.ProductsEndpoint, setToken: true);
        }

        public static JArray GetCategories()
        {
            return HttpGetArray(Settings.CategoriesEndpoint, setToken: true);
        }

        public static JArray GetColors()
        {
            return HttpGetArray(Settings.ColorsEndpoint, setToken: true);
        }

        public static JObject GetUserPrinciple()
        {
            return HttpGetObject(Settings.UsersEndpoint + "/current", setToken: true);
        }

        public static JArray GetUsers()
        {
            return HttpGetArray(Settings.UsersEndpoint, setToken: true);
        }


        private static JArray HttpGetArray(string uri, bool setToken = false)
        {
            var client = new HttpClient();

            if(setToken)
            {
                client.SetBearerToken(TokenService.CurrentTokenData?.AccessToken);
            }
            string response = client.GetStringAsync(uri).Result;

            return JArray.Parse(response);
        }

        private static JObject HttpGetObject(string uri, bool setToken = false)
        {
            var client = new HttpClient();

            if (setToken)
            {
                client.SetBearerToken(TokenService.CurrentTokenData?.AccessToken);
            }
            string response = client.GetStringAsync(uri).Result;

            return JObject.Parse(response);
        }

    }
}
