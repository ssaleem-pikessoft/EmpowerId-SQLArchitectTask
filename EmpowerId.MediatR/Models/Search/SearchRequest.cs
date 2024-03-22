using EmpowerId.Entities.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.MediatR.Models.Search
{
    public class SearchRequest : IRequest<RequestResponse>
    {
        [Required(ErrorMessage = "Search cannot be empty.")]
        public string SearchTerm { get; set; } = string.Empty;
    }
}
