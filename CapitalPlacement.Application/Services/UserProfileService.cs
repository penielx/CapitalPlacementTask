using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapitalPlacement.Application.Interfaces;
using CapitalPlacement.Shared.Responses;

namespace CapitalPlacement.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository userProfileRepository;
        public UserProfileService(IUserProfileRepository _userProfileRepository)
        {
            userProfileRepository = _userProfileRepository;
        }

        public async Task<StandardResponse<UserProfile>> AddNewUser(UserProfile userProfile)
        {
            var response = await userProfileRepository.AddNewUser(userProfile);
            if(response is null) return StandardResponse<UserProfile>.Failed("Unable to add user", response);

            return StandardResponse<UserProfile>.Succeeded("New user was addedd successfully", response);
        }

        public async Task<StandardResponse<UserProfile>> DeleteUserProfileAsync(string userId, string IDNumber)
        {
            var response = await userProfileRepository.DeleteUserProfileAsync(userId, IDNumber);

            return StandardResponse<UserProfile>.Succeeded("User profile was deleted successfully", null);
        }

      
        public async Task<StandardResponse<IEnumerable<UserProfile>>> GetAllUser()
        {
            var response = await userProfileRepository.GetAllUser();
            if (response is null) return StandardResponse<IEnumerable<UserProfile>>.Failed("Unable to retrieve user", response);
            return StandardResponse<IEnumerable<UserProfile>>.Succeeded("All Users", response);
        }

        public async Task<StandardResponse<UserProfile>> GetUserByIdAsync(string appId)
        {
            var response = await userProfileRepository.GetUserByIdAsync(appId);
            if (response is null) return StandardResponse<UserProfile>.Failed("Unable to retrieve user", response);
            return StandardResponse<UserProfile>.Succeeded("User by Id", response);
        }

        public async Task<StandardResponse<List<UserProfile>>> SearchUserByNameOrEmail(string searchTerm)
        {
            var response = await userProfileRepository.SearchUserByNameOrEmail(searchTerm);
            if (response is null) return StandardResponse<List<UserProfile>>.Failed("Unable to retrieve user", 400);
            return StandardResponse<List<UserProfile>>.Succeeded("List of searched users", response, 200);
        }

        public async Task<StandardResponse<UserProfile>> UpdateUserProfileAsync(UserProfile userProfile, string userId)
        {
            var getUser = await userProfileRepository.GetUserByIdAsync(userId);
            if (getUser is null)
            {
              return  StandardResponse<UserProfile>.Failed("Unable to find user", 400);
            }

            getUser.UpdatedDate = DateTime.Now;
            getUser.Firstname = userProfile.Firstname;
            getUser.Lastname = userProfile.Lastname;
            getUser.Email = userProfile.Email;
            getUser.IDNumber = userProfile.IDNumber;
            getUser.Nationality = userProfile.Nationality;
            getUser.CurrentResidence = userProfile.CurrentResidence;
            getUser.Gender = userProfile.Gender;
            getUser.DateofBirth = userProfile.DateofBirth;
            var response = await userProfileRepository.UpdateUserProfileAsync(userProfile);
            if (response is null) return StandardResponse<UserProfile>.Failed("Unable to update user", 400);
            return StandardResponse<UserProfile>.Succeeded("User Updated successfully!", response, 200);

        }

    }
}
