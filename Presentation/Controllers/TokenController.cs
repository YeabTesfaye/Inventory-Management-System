using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;
[Route("api/token")]
[ApiController]

public class TokenController : ControllerBase
{
    private readonly IServiceManager _services;
    public TokenController(IServiceManager services) => _services = services;

    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        Console.WriteLine(tokenDto);
        var tokenDtoToReturn = await
        _services.AuthenticationService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }

}