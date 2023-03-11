using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Util;

namespace Shared.ControllerBase
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ReturnActionResult(Response response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}