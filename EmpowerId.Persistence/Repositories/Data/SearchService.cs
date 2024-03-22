using EmpowerId.Entities;
using EmpowerId.Entities.Requests;
using EmpowerId.Interfaces.Contracts.Persistence.Data;
using EmpowerId.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Persistence.Repositories.Data
{
    internal sealed class SearchService : ISearchService
    {
        private readonly EmpowerDbContext _dbContext;
        public SearchService(EmpowerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<RequestResponse> Search(string searchTerm)
        {
            RequestResponse response = new();
            try
			{
                var categoriesResult = await _dbContext.Categories
                                             .Where(category => category.CategoryId.ToString().Contains(searchTerm) ||
                                              category.CategoryName.Contains(searchTerm) ||
                                              category.CreatedDate.ToString("yyyy-MM-dd").Contains(searchTerm))
                                              .Select(category => new
                                              {
                                                  Type = "Category",
                                                  Name = category.CategoryName,
                                                  category.CreatedDate
                                              })
                                              .ToListAsync();
                // Search in products list
                var productsResult = await _dbContext.Products
                    .Where(product => product.ProductId.ToString().Contains(searchTerm) ||
                                      product.CategoryId.ToString().Contains(searchTerm) ||
                                      product.ProductName.Contains(searchTerm) ||
                                      (product.Description != null && product.Description.Contains(searchTerm)) ||
                                      product.Price.ToString().Contains(searchTerm) ||
                                      product.CreatedDate.ToString("yyyy-MM-dd").Contains(searchTerm))
                    .Select(product => new
                    {
                        Type = "Product",
                        Name = product.ProductName,
                        Description = product.Description ?? "",
                        product.Price,
                        ImageUrl = product.ImageUrl ?? "",
                        product.CreatedDate
                    })
                    .ToListAsync();

                var searchResults = new List<object>();
                if (categoriesResult != null)
                    searchResults.AddRange(categoriesResult);
                if (productsResult != null)
                    searchResults.AddRange(productsResult);
                response.Data = searchResults;
            }
            catch (Exception ex)
			{

			}
            return response;
        }
    }
}
