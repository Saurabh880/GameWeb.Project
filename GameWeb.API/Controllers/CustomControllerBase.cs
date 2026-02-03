using Microsoft.AspNetCore.Mvc;

namespace GameWeb.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class CustomControllerBase : ControllerBase
    {

    }
}