        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Mvc;
        using Services.Abstraction.Contracts;
        using Shared.Dtos.IdentityModule;
        using Shared.Dtos.OrderModule;
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Security.Claims;
        using System.Text;
        using System.Threading.Tasks;

        namespace Presentation.Controller
        {
            public class AuthenticationController(IServiceManager _serviceManager) : ApiController
            {
                //pose register
                [HttpPost("Register")]
                public async Task<ActionResult<UserResultDto>>RegisterAsunc(RegisterDto registerDto)
                    => Ok (await _serviceManager.AutenticationService.RegisterAsync(registerDto));
                //post Login
                [HttpPost("Login")]

                public async Task<ActionResult<UserResultDto>> LoginAsunc(LoginDto LoginDto)
                   => Ok(await _serviceManager.AutenticationService.LoginAsync(LoginDto));

                [HttpGet("EmailExist")]
                public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
                {
                    return Ok(
                        await _serviceManager.AutenticationService
                            .CheckIfEmailExistAsync(email));
                }

                [Authorize]
                [HttpGet]
                public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
                {
                    var email = User.FindFirstValue(ClaimTypes.Email);

                    if (email is null)
                        return Unauthorized();

                    var user = await _serviceManager.AutenticationService
                        .GetCurrentUserAsync(email);

                    return Ok(user);
                }

                [Authorize]
                [HttpGet("Address")]
                public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
                {
                    var email = User.FindFirstValue(ClaimTypes.Email);

                    if (email is null)
                        return Unauthorized();

                    var address = await _serviceManager.AutenticationService
                        .GetUserAddressAsync(email);

                    return Ok(address);
                }

                [Authorize]
                [HttpPut("Address")]
                public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(
                    AddressDto addressDto)
                {
                    var email = User.FindFirstValue(ClaimTypes.Email);

                    if (email is null)
                        return Unauthorized();

                    var address = await _serviceManager.AutenticationService
                        .UpdateaAddressAsync(email, addressDto);

                    return Ok(address);
                }
            }
        }
