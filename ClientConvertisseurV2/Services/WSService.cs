﻿using ClientConvertisseurV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace ClientConvertisseurV2.Services
{
    public class WSService : IService
    {
        private HttpClient client;

        public WSService(string url)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(url);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }

        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await Client.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
