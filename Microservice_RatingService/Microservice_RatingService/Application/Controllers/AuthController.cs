using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static Microservice_RatingService.JwtSettings;

namespace Microservice_RatingService.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IJwtService jwtService, ILogger<AuthController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }

        public class LoginRequest
        {
            [Required]
            public required string Username { get; set; }
            [Required]
            public required string Password { get; set; }
        }

        public class LoginResponse
        {
            [Required]
            public required string Token { get; set; }
            [Required]
            public required string Username { get; set; }
            [Required]
            public required string[] Roles { get; set; }
            public DateTime ExpiresAt { get; set; }
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Generate JWT token for testing")]
        [SwaggerResponse(200, "Login successful, returns JWT token")]
        [SwaggerResponse(400, "Invalid credentials")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login attempt for user: {Username}", request.Username);

            // For testing - in production, validate against your user database
            string[] roles;
            if (request.Username == "admin" && request.Password == "adminpass")
            {
                roles = new[] { "Admin" };
            }
            else if (request.Username == "testuser" && request.Password == "testpass")
            {
                roles = new[] { "User" };
            }
            else
            {
                return BadRequest("Invalid credentials");
            }

            var token = _jwtService.GenerateToken(
                Guid.NewGuid().ToString(),
                request.Username,
                roles
            );

            var response = new LoginResponse
            {
                Token = token,
                Username = request.Username,
                Roles = roles,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };

            return Ok(response);
        }
    }
}
