using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

/// <summary>
/// Provides endpoints for managing authentication tokens.
/// </summary>
[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IServiceManager _services;

    public TokenController(IServiceManager services) => _services = services;

    /// <summary>
    /// Refreshes an authentication token.
    /// </summary>
    /// <param name="tokenDto">The token data transfer object containing the refresh token.</param>
    /// <returns>Returns a new token DTO with updated authentication tokens.</returns>
    /// <response code="200">Returns the new token DTO with refreshed tokens.</response>
    /// <response code="400">If the token data is invalid or missing.</response>
    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        // Log the incoming token DTO for debugging purposes
        Console.WriteLine(tokenDto);

        // Refresh the token using the authentication service
        var tokenDtoToReturn = await _services.AuthenticationService.RefreshToken(tokenDto);

        // Return the new token DTO
        return Ok(tokenDtoToReturn);
    }
}
