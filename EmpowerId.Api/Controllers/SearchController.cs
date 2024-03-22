using EmpowerId.Entities;
using EmpowerId.Entities.Requests;
using EmpowerId.MediatR.Models.Search;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace EmpowerId.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// The constructor initializes an instance of the <see cref="SearchController"/> class, which is responsible for handling search-related endpoints in the API. It takes an IMediator parameter, allowing for mediation of requests and responses.
        /// </summary>
        /// <param name="mediator">An implementation of the IMediator interface, which facilitates the decoupling of components by mediating requests and responses.</param>
        public SearchController(IMediator mediator)
               : base(mediator)
        {
        }

        // GET api/<SearchController>/search
        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<RequestResponse>> Get([Required()] string searchTerm)
        {
            RequestResponse? requestResponse = new();
            try
            {
                requestResponse = await this.Request.Send(new SearchRequest() { SearchTerm = searchTerm });

            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(ex.Message, EventLogEntryType.Information, 101, 1);
                }
            }

            return this.Ok(requestResponse);
        }

        // GET api/<SearchController>/search
        //[HttpGet("{searchTerm}")]
        //public ActionResult Get(string searchTerm)
        //{
        //    List<Category> Categories =
        //    [
        //        new()
        //        {
        //            CategoryId = Guid.NewGuid(),
        //            CategoryName = "Clothing",
        //            CreatedDate = DateTime.UtcNow
        //        },
        //        new()
        //        {
        //            CategoryId = Guid.NewGuid(),
        //            CategoryName = "Electronics",
        //            CreatedDate = DateTime.UtcNow
        //        }
        //    ];
        //    List<Product> products =
        //    [
        //        new()
        //        {
        //            ProductId = Guid.NewGuid(),
        //            CategoryId = Categories[0].CategoryId,
        //            ProductName = "Laptop",
        //            Description = "High-performance laptop",
        //            Price = 999.99f,
        //            ImageUrl = "laptop.jpg",
        //            CreatedDate = DateTime.UtcNow,
        //            Category = Categories[0]
        //        },
        //        new()
        //        {
        //            ProductId = Guid.NewGuid(),
        //            CategoryId = Categories[1].CategoryId,
        //            ProductName = "T-Shirt",
        //            Description = "Cotton t-shirt",
        //            Price = 19.99f,
        //            ImageUrl = "tshirt.jpg",
        //            CreatedDate = DateTime.UtcNow,
        //            Category = Categories[1]
        //        }
        //    ];

        //    var categoriesResult = Categories
        //   .Where(category => category.CategoryId.ToString().Contains(searchTerm) ||
        //                       category.CategoryName.Contains(searchTerm) ||
        //                       category.CreatedDate.ToString("yyyy-MM-dd").Contains(searchTerm))
        //   .Select(category => new
        //   {
        //       Type = "Category",
        //       Name = category.CategoryName,
        //       category.CreatedDate
        //   })
        //   .ToList();

        //    // Search in products list
        //    var productsResult = products
        //        .Where(product => product.ProductId.ToString().Contains(searchTerm) ||
        //                          product.CategoryId.ToString().Contains(searchTerm) ||
        //                          product.ProductName.Contains(searchTerm) ||
        //                          product.Description?.Contains(searchTerm) == true ||
        //                          product.Price.ToString().Contains(searchTerm) ||
        //                          product.ImageUrl?.Contains(searchTerm) == true ||
        //                          product.CreatedDate.ToString("yyyy-MM-dd").Contains(searchTerm))
        //        .Select(product => new
        //        {
        //            Type = "Product",
        //            Name = product.ProductName,
        //            Description = product.Description ?? "",
        //            product.Price,
        //            ImageUrl = product.ImageUrl ?? "",
        //            product.CreatedDate
        //        })
        //        .ToList();

        //    var searchResults = new List<object>();
        //    if (categoriesResult != null)
        //        searchResults.AddRange(categoriesResult);
        //    if (productsResult != null)
        //        searchResults.AddRange(productsResult);
           

        //    // Serialize response to JSON format
        //    string jsonResponse = System.Text.Json.JsonSerializer.Serialize(searchResults);


        //    return Ok(jsonResponse);
        //}

    }
}
