using AutoMapper;
using Florin_Back.DTOs.Auth;
using Florin_Back.DTOs.User;
using Florin_Back.Models;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMapper mapper, IAuthService authService) : ControllerBase
    {
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var mappedUser = mapper.Map<User>(registerDto);
            var registeredUser = await authService.RegisterAsync(mappedUser);
            var userDto = mapper.Map<UserDTO>(registeredUser);

            return CreatedAtAction(nameof(Register), userDto);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var mapperUser = mapper.Map<User>(loginDto);
            await authService.LoginAsync(mapperUser);

            return NoContent();
        }

        [HttpDelete(nameof(Logout))]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();

            return NoContent();
        }
    }
}
