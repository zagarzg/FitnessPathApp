using Microsoft.AspNetCore.Mvc;

namespace FitnessPathApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
