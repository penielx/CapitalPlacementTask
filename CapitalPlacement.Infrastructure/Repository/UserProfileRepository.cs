
using System.Collections.Generic;

namespace CapitalPlacement.Infrastructure.Repository;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly Container _userProfileContainer;
    private readonly IConfiguration configuration;

    public UserProfileRepository(CosmosClient cosmosClient, IConfiguration configuration)
    {
        this.configuration = configuration;

        var databaseName = configuration["Cosmos:DatabaseId"];
        var taskContainerName = "UserProfile";
        _userProfileContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
    }

    public async Task<IEnumerable<UserProfile>> GetAllUser()
    {
        var query = _userProfileContainer.GetItemLinqQueryable<UserProfile>().ToFeedIterator();
        var response = await query.ReadNextAsync();
        return response.ToList();
    }

    public async Task<UserProfile> GetUserByIdAsync(string userid)
    {

        var query = _userProfileContainer.GetItemLinqQueryable<UserProfile>()
        .Where(t => t.Id.ToString() == userid)
        .Take(1)
        .ToQueryDefinition();

        var sqlQuery = query.QueryText;

        var response = await _userProfileContainer.GetItemQueryIterator<UserProfile>(query).ReadNextAsync();
        return response.FirstOrDefault();
    }

    public async Task<List<UserProfile>> SearchUserByNameOrEmail(string searchTerm)
    {
        var queryable = _userProfileContainer.GetItemLinqQueryable<UserProfile>();

        var query = queryable
            .Where(u => u.Firstname.Contains(searchTerm) || u.Email.Contains(searchTerm))
            .ToFeedIterator();

        var results = new List<UserProfile>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task<UserProfile> AddNewUser(UserProfile newUser)
    {
        var response = await _userProfileContainer.CreateItemAsync(newUser);
        return response.Resource;
    }

    public async Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile)
    {
        var response = await _userProfileContainer.ReplaceItemAsync(userProfile, userProfile.Id.ToString());
        return response.Resource;
    }

    public async Task<bool> DeleteUserProfileAsync(string userId, string IDNumber)
    {
        try
        {
            var response = await _userProfileContainer.DeleteItemAsync<UserProfile>(userId, new PartitionKey(IDNumber));
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        catch (CosmosException ex)
        {
            return false;
        }
    }
}

