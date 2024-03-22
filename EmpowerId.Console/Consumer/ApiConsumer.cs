using EmpowerId.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Console.Consumer
{
    internal class ApiConsumer
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        
        public ApiConsumer(string baseApi)
        {
            _httpClient = new HttpClient();
            _baseApi = baseApi;
        }

        public async Task<RequestResponse> ConsumeApiAsync(string searchTerm)
        {
            RequestResponse response = new();
            try
            {
                string apiUrl = $"{_baseApi}/Search/{searchTerm}";
                HttpResponseMessage result = await _httpClient.GetAsync(apiUrl);
                if (result != null && result.IsSuccessStatusCode)
                {
                    // Read and return the response content
                    response = await result.Content.ReadFromJsonAsync<RequestResponse>();
                }
                else
                {
                    response.Message = $"Error: {response.StatusCode}";
                }
            }
            catch(Exception ex) 
            { 
               
            }
            return response;
        }
    }
}
