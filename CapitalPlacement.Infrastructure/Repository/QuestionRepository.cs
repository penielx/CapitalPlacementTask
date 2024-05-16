
namespace CapitalPlacement.Infrastructure.Repository;

    public class QuestionRepository : IQuestionRepository
    {
        private readonly Container _taskContainer;
        private readonly IConfiguration configuration;

        public QuestionRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            this.configuration = configuration;

            var databaseName = configuration["Cosmos:DatabaseId"];
            var taskContainerName = "ApplicationBank";
            _taskContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
        }

        public async Task<IEnumerable<ApplicationBank>> GetAllQuestionsAsync()
        {
            var query = _taskContainer.GetItemLinqQueryable<ApplicationBank>().ToFeedIterator();
            var response = await query.ReadNextAsync();
            return response.ToList();
        }

        public async Task<ApplicationBank> GetApplicationByIdAsync(string applicationId)
        {

            var query = _taskContainer.GetItemLinqQueryable<ApplicationBank>()
            .Where(t => t.Id.ToString() == applicationId)
            .Take(1)
            .ToQueryDefinition();

            var sqlQuery = query.QueryText;

            var response = await _taskContainer.GetItemQueryIterator<ApplicationBank>(query).ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<ApplicationBank> GetApplicationByIsDefault(bool IsDefault)
        {
            var query = _taskContainer.GetItemLinqQueryable<ApplicationBank>()
            .Where(t => t.IsDefaultApplication == IsDefault)
            .Take(1)
            .ToQueryDefinition();

            var sqlQuery = query.QueryText;

            var response = await _taskContainer.GetItemQueryIterator<ApplicationBank>(query).ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<ApplicationBank> AddNewApplication(ApplicationBank applicationBank)
        {
            var response = await _taskContainer.CreateItemAsync(applicationBank);
            return response.Resource;
        }

        public async Task<ApplicationBank> AddNewApplicationQuestions(ApplicationQuestions applicationQuestions, string ApplicationBankId)
        {
            var query = _taskContainer.GetItemLinqQueryable<ApplicationBank>()
           .Where(t => t.Id.ToString() == ApplicationBankId)
           .Take(1)
           .ToQueryDefinition();

            var sqlQuery = query.QueryText;

            var response = await _taskContainer.GetItemQueryIterator<ApplicationBank>(query).ReadNextAsync();
            var appQuestionDetails = response.FirstOrDefault();

            if (appQuestionDetails != null)
            {
                if (appQuestionDetails.ApplicationQuestions == null)
                    appQuestionDetails.ApplicationQuestions = new List<ApplicationQuestions>();

                appQuestionDetails.ApplicationQuestions.Add(applicationQuestions);
                appQuestionDetails.UpdatedDate = DateTime.Now;

                var result = await _taskContainer.ReplaceItemAsync(appQuestionDetails, appQuestionDetails.Id.ToString());
                return result.Resource;
            }

            //appQuestionDetails.ApplicationQuestions.Add(applicationQuestions);
            //var result = await _taskContainer.ReplaceItemAsync(appQuestionDetails, appQuestionDetails.Id.ToString());
            return null;
        }

        public async Task<ApplicationBank> UpdateApplicationBankAsync(ApplicationBank task)
        {
            var response = await _taskContainer.ReplaceItemAsync(task, task.Id.ToString());
            return response.Resource;
        }

        public async Task DeleteApplicationbankAsync(string appId, string name)
        {
            await _taskContainer.DeleteItemAsync<ApplicationBank>(appId, new PartitionKey(name));
        }
    }

