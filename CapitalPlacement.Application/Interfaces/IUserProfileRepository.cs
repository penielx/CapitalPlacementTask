using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacement.Application.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> AddNewUser(UserProfile newUser);
        Task<bool> DeleteUserProfileAsync(string userId, string IDNumber);
        Task<IEnumerable<UserProfile>> GetAllUser();
        Task<UserProfile> GetUserByIdAsync(string userid);
        Task<List<UserProfile>> SearchUserByNameOrEmail(string searchTerm);
        Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile);
    }
}
