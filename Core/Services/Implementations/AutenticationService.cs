using AutoMapper;
using Domain.Entity.IdentityModule;

using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Commen;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class AutenticationService(UserManager<User> _userManager,IOptions<JwtOptions> _options ,IMapper _mapper) : IAutenticationService
    {
        public async Task<bool> CheckIfEmailExistAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            return user != null;
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail) ?? throw new UserNotFoundException(userEmail);

            return new UserResultDto(
                user.DisplayName,
                await CreateTokenAsync(user),
                user.Email
            );
        }

        public async Task<AddressDto> GetUserAddressAsync(string userEmail)
        {
            var user = await _userManager.Users
        .Include(user => user.Adress)
        .FirstOrDefaultAsync(u => u.Email == userEmail)
        ?? throw new UserNotFoundException(userEmail);

            return _mapper.Map<AddressDto>(user.Adress);
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)  throw new UnauthorizedException();

            // Check password
            var result = await _userManager.CheckPasswordAsync( user, loginDto.Password);

            if (!result)
                throw new UnauthorizedException();

            return new UserResultDto(user.DisplayName,await CreateTokenAsync(user), user.Email);

        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            // Validate
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationsException(errors);
            }

            return new UserResultDto(
                user.DisplayName,
                await CreateTokenAsync(user),
                user.Email);
        }

        public async Task<AddressDto> UpdateaAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await _userManager.Users
        .Include(user => user.Adress)
        .FirstOrDefaultAsync(u => u.Email == userEmail)
        ?? throw new UserNotFoundException(userEmail);

            if (user.Adress != null)
            {
                // Update existing address
                user.Adress.FirstName = addressDto.FirstName;
                user.Adress.LastName = addressDto.LastName;
                user.Adress.Country = addressDto.Country;
                user.Adress.City = addressDto.City;
                user.Adress.Street = addressDto.Street;
            }
            else
            {
                // Create new address
                user.Adress = _mapper.Map<Adress>(addressDto);
            }

            await _userManager.UpdateAsync(user);

            return _mapper.Map<AddressDto>(user.Adress  );
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwetOption = _options.Value;
            // Claims

            var claims = new List<Claim>
             {
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.Email, user.Email!)
            };

            // Roles
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Secret key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwetOption.SecretKey));

            // Signing credentials
            var signInCreds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            // Create token
            var token = new JwtSecurityToken(
            issuer: jwetOption.Issuer,
            audience: jwetOption.Audience,
            claims: claims,
             expires: DateTime.UtcNow.AddDays(jwetOption.ExpirationInDays),
                signingCredentials: signInCreds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
