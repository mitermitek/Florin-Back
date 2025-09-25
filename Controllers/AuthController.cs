using AutoMapper;
using Florin_Back.Models.DTOs.User;
using Florin_Back.Models.DTOs.Auth;
using Florin_Back.Models.Entities;
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
            var userToRegister = mapper.Map<User>(registerDto);
            var registeredUser = await authService.RegisterAsync(userToRegister);
            var userDTO = mapper.Map<UserDTO>(registeredUser);

            return CreatedAtAction(nameof(Register), userDTO);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var userToLogIn = mapper.Map<User>(loginDto);
            var user = await authService.LoginAsync(userToLogIn);
            var userDTO = mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        [HttpDelete(nameof(Logout))]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();

            return NoContent();
        }

        [HttpGet(nameof(Status))]
        [Authorize]
        public async Task<IActionResult> Status()
        {
            var currentUser = await authService.GetCurrentUserAsync();
            var currentUserDTO = mapper.Map<UserDTO>(currentUser);

            return Ok(currentUserDTO);
        }
    }
}
