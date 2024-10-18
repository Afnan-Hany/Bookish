using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YatApp.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IUnitofWork _unitofWork;

        public BaseApiController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
    }
}
