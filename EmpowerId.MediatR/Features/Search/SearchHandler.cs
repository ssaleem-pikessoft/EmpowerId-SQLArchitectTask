using Azure.Search.Documents;
using Azure;
using EmpowerId.Entities.OptionsSetup;
using EmpowerId.Entities.Requests;
using EmpowerId.Interfaces.Contracts.Persistence.Data;
using EmpowerId.MediatR.Models.Search;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Search.Documents.Models;
using Microsoft.Azure.Search.Models;
using System.Text.Json;

namespace EmpowerId.MediatR.Features.Search
{
    public class SearchHandler: IRequestHandler<SearchRequest, RequestResponse>
    {
        private readonly ISearchService _SearchService;
        private readonly SearchClientOption _searchClientOption;

        public SearchHandler(ISearchService searchService, IOptions<SearchClientOption> searchClientOption)
        {
            _SearchService = searchService;
            _searchClientOption = searchClientOption.Value;
        }
        public async Task<RequestResponse> Handle(SearchRequest request, CancellationToken cancellationToken)
        {
            RequestResponse resp = new();
            try
            {
                string key = Environment.GetEnvironmentVariable(_searchClientOption.VariableName);
                if(string.IsNullOrEmpty(key))
                {
                    resp.Message = "Application confiuragtion not properly";
                    return resp;

                }
                Uri endpoint = new Uri(_searchClientOption.Endpoint);
                string indexName = _searchClientOption.Indexname;

                // Create a client
                AzureKeyCredential credential = new(key);
                SearchClient client = new(endpoint, indexName, credential);
                var searchResults =  await client.SearchAsync<SearchDocument>(request.SearchTerm, new SearchOptions()
                {
                    SearchMode = Azure.Search.Documents.Models.SearchMode.Any,
                    QueryType = SearchQueryType.Full
                });
                foreach (Azure.Search.Documents.Models.SearchResult<SearchDocument> result in searchResults.Value.GetResults())
                {
                    Console.WriteLine($"Score: {result.Score}");

                    foreach (KeyValuePair<string, object> field in result.Document)
                    {
                        Console.WriteLine($"{field.Key}: {field.Value}");
                    }
                    Console.WriteLine();
                }

                resp.Data = JsonSerializer.Serialize(searchResults.Value.GetResults());

            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.StatusCode = 500;
                resp.Errors = ex;
            }
            return resp;
        }
    }
}
