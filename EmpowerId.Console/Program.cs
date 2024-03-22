using EmpowerId.Console.Consumer;
using EmpowerId.Entities.Requests;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.Json;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.console.json", optional: true, reloadOnChange: true)
    .Build();

string baseApi = configuration["ConsoleSettings:BaseApi"];
if(baseApi == null)
{
    Console.WriteLine("configure the parameters correctly.");
}

var apiConsumer = new ApiConsumer(baseApi);
Console.WriteLine("Welcome to the API Search Tool!");
Console.WriteLine("Please enter your search term, or press Ctrl+C to exit:");

while (true)
{
    try
    {
        // Read user input
        string searchTerm = Console.ReadLine(); 

        // If user input is null, it means Ctrl+C was pressed
        if (searchTerm == null)
        {
            Console.WriteLine("Exiting the application...");
            break;
        }

        // Consume the API with the search term
        RequestResponse apiResponse = await apiConsumer.ConsumeApiAsync(searchTerm);
        Console.WriteLine();
        Console.WriteLine($"Result for search term '{searchTerm}':");
        Console.WriteLine("-------------------------------------------");

        if (apiResponse.IsValid)
        {
            if (apiResponse.Data != null)
            {
                JsonElement jsonData = JsonSerializer.Deserialize<JsonElement>(apiResponse.Data.ToString());
                // Check if jsonData is not null and contains any items
                if (jsonData.ValueKind == JsonValueKind.Array && jsonData.GetArrayLength() > 0)
                {
                    foreach (JsonElement result in jsonData.EnumerateArray())
                    {
                        if (result.TryGetProperty("Document", out JsonElement document))
                        {
                            // Iterate over each property of the document object
                            foreach (JsonProperty property in document.EnumerateObject())
                            {
                                Console.WriteLine($"{property.Name}:   {property.Value}");
                            }
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Record Found!");
                }
            }
            else
            {
                Console.WriteLine("No Record Found!");
            }
        }
        else
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(apiResponse.Message, EventLogEntryType.Information, 101, 1);
            }
        }

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Please enter your search term, or press Ctrl+C to exit:");
    }
    catch (Exception ex) 
    {
        Console.WriteLine("Please try selecting another.");
    }
}
