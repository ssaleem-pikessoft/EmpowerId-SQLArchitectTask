using EmpowerId.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmpowerId.Interfaces.Contracts.Persistence.Data
{
    public interface ISearchService
    {
        Task<RequestResponse> Search(string searchTerm);
    }
}
