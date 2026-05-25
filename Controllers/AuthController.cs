using Microsoft.AspNetCore.Mvc;
using Authservice.DTOs;
using Authservice.Models;
using Authservice.Service;
using System.Reflection.Metadata.Ecma335;


namespace Authservice.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

       [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var existingUser = await _userService.GetUserByEmailAsync(registerDTO.Email);

            if (existingUser != null)
            {
                return BadRequest("Account already exists with this email.");
            }

            var user = new User
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                Role = "User"
            };

            await _userService.AddUserAsync(user);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userService.GetUserByEmailAsync(loginDTO.Email);

            if (user == null || user.Password != loginDTO.Password)
                return Unauthorized("Invalid email or password.");

            var token = _jwtService.GenerateToken(user);
            return Ok(
                new
                {
                    Message = $"{user.Role} logged in successfully",
                    Role = user.Role,
                    Token = token
                }
            );
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userService.GetUserByEmailAsync(forgotPasswordDTO.Email);

            if (user == null)
                return BadRequest("User not found.");

            return Ok("Password reset link sent (mock).");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var user = await _userService.GetUserByEmailAsync(dto.Email);

            if (user == null)
                return BadRequest("User not found.");

            user.Password = dto.NewPassword;

            await _userService.UpdateUserAsync(user);

            return Ok("Password reset successful.");
        }
    }
}