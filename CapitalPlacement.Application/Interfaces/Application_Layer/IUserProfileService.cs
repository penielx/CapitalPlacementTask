using CapitalPlacement.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacement.Application.Interfaces.Application_Layer
{
    public interface IUserProfileService
    {
        Task<StandardResponse<UserProfile>> AddNewUser(UserProfile userProfile);
        Task<StandardResponse<UserProfile>> DeleteUserProfileAsync(string userId, string IDNumber);
        Task<StandardResponse<IEnumerable<UserProfile>>> GetAllUser();

        Task<StandardResponse<UserProfile>> GetUserByIdAsync(string appId);

        Task<StandardResponse<List<UserProfile>>> SearchUserByNameOrEmail(string searchTerm);

        Task<StandardResponse<UserProfile>> UpdateUserProfileAsync(UserProfile userProfile, string userId);
    }
}
