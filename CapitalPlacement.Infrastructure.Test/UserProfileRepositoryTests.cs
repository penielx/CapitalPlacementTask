using CapitalPlacement.Domain;
using CapitalPlacement.Infrastructure.Repository;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapitalPlacement.Infrastructure.Test
{
    [TestClass]
    public class UserProfileRepositoryTests
    {

        [TestMethod]
        public async Task GetAllUser_ShouldReturnAllUsers()
        {

            var mockContainer = new Mock<Container>();
            var mockCosmosClient = new Mock<CosmosClient>();
            var mockConfiguration = new Mock<IConfiguration>();

            // Mock response data
            var userProfiles = new List<UserProfile>
            {
            new UserProfile { Id = Guid.NewGuid(), Firstname = "User 1", Lastname = "Last Name", Email = "test@admin.com", PhoneNumber = "0804376374", DateofBirth = DateTime.Now.AddYears(-20), Gender = "Male" },
            new UserProfile { Id = Guid.NewGuid(), Firstname = "User 2", Lastname = "Last Name2", Email = "test2@admin.com", PhoneNumber = "0804376374434", DateofBirth = DateTime.Now.AddYears(-22), Gender = "Female" },

             };
            var mockFeedResponse = new Mock<FeedResponse<UserProfile>>();
            mockFeedResponse.Setup(x => x.GetEnumerator()).Returns(userProfiles.GetEnumerator());

            var mockIterator = new Mock<FeedIterator<UserProfile>>();
            mockIterator.Setup(x => x.HasMoreResults).Returns(false);
            mockIterator.Setup(x => x.ReadNextAsync(default)).ReturnsAsync(mockFeedResponse.Object);

            mockIterator.Setup(x => x.ReadNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockFeedResponse.Object);

            var databaseName = "Cosmos:DatabaseId";
            mockCosmosClient.Setup(x => x.GetContainer(databaseName, "UserProfile")).Returns(mockContainer.Object);

            var userProfileRepository = new UserProfileRepository(mockCosmosClient.Object, mockConfiguration.Object);

            var result = await userProfileRepository.GetAllUser();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
                                                
        }

    }
}
