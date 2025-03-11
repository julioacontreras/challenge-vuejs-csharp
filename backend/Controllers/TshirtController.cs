using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tshirt")]
public class TshirtController : ControllerBase
{
    [HttpGet]
    public IActionResult GetMessage()
    {
        return Ok(new { message = "Is working!" });
    }
}