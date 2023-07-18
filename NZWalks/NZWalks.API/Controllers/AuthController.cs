﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.Auth;
using NZWalks.API.Repositories.TokenRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,
                               ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            IdentityUser identityUser = new()
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            IdentityResult identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (registerRequestDTO.Roles?.Any() == true)
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login. ");
                    }
                }
            }

            return BadRequest("Something went wrong.");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            IdentityUser? user = await userManager.FindByEmailAsync(loginRequestDTO.Username);
            BadRequestObjectResult badRequest = BadRequest("Username or Password is wrong.");
            
            if (user == null)
            {
                return badRequest;
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (isPasswordCorrect == false)
            {
                return badRequest;
            }

            // Get Roles for this user
            IList<string> roles = await userManager.GetRolesAsync(user);

            if (roles != null)
            {
                // CREATE TOKEN
                string jwtToken = tokenRepository.CreateJWTToken(user, roles);

                return Ok(new LoginResponseDTO { JwtToken = jwtToken });
            }

            return badRequest;
        }
    }
}
