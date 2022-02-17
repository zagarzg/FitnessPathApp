using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessPathApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
