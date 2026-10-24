using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IAutenticationService
    {
        //login
        Task<UserResultDto> LoginAsync (LoginDto loginDto);

        //register
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        //Get current user
        Task<UserResultDto> GetCurrentUserAsync(string userEmaiL);

        //check if email exist
        Task<bool> CheckIfEmailExistAsync(string userEmail);

        //GetAddress
        Task<AddressDto> GetUserAddressAsync(string userEmail);
        //Update address
        Task<AddressDto> UpdateaAddressAsync(string userEmail , AddressDto addressDto  );
    }
}
