using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpowerId.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        protected new IMediator Request
        {
            get { return this._mediator; }
        }
    }
}
