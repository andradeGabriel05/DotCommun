using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController
{
    [HttpPost]
    public void Post([FromBody] string value)
    {
        
    }
}