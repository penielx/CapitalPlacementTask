using CapitalPlacement.Domain;
using CapitalPlacement.Shared.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _appService;

        public UserProfileController(IUserProfileService userServ)
        {
            _appService = userServ;
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
            var user = await _appService.GetAllUser();
            return Ok(user);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _appService.GetUserByIdAsync(userId);
            return Ok(user);
        }


        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser(UserProfileDto userProfile)
        {
            var user = await _appService.AddNewUser(userProfile.Adapt<UserProfile>());
            return Ok(user);
        }

        [HttpPost("SearchUserByNameOrEmail")]
        public async Task<IActionResult> SearchUserByNameOrEmail(string searchTerm)
        {
            var user = await _appService.SearchUserByNameOrEmail(searchTerm);
            return Ok(user);
        }

        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfileAsync(UserProfileDto userProfileDto, string userId)
        {
            var user = await _appService.UpdateUserProfileAsync(userProfileDto.Adapt<UserProfile>(), userId);
            return Ok(user);
        }

        [HttpDelete("DeleteUserProfile")]
        public async Task<IActionResult> DeleteUserProfile(string userId, string IdNumber)
        {
            var user = await _appService.DeleteUserProfileAsync(userId, IdNumber);
            return Ok(user);
        }


    }
}
