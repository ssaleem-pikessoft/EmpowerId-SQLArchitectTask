using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Entities.Requests
{
    public class RequestResponse
    {
        public string? Message { get; set; } = "success";
        public object? Data { get; set; }
        public bool IsValid { get; set; } = true;
        public object? Errors { get; set; }
        public int StatusCode { get; set; } = 200;
    }
}
