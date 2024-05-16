
namespace CapitalPlacement.API.Extension;
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("https://localhost:3000", "http://localhost:5000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IApplicationBankService, ApplicationBankService>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
        }
        public static async Task ConfigureCosmos(this IServiceCollection services, IConfiguration configuration)
        {
            var endpointUri = configuration["Cosmos:AccountURL"];
            var primaryKey = configuration["Cosmos:AuthKey"];
            var databaseName = configuration["Cosmos:DatabaseId"];

            var cosmosClientOptions = new CosmosClientOptions
            {
                ApplicationName = databaseName,
                ConnectionMode = ConnectionMode.Direct,
            };

            var cosmosClient = new CosmosClient(endpointUri, primaryKey, cosmosClientOptions);
            var database = cosmosClient.GetDatabase(databaseName);

            var containerDefinitions = new Dictionary<string, string>
                 {
                     { "UserProfile", "/IDNumber" }, 
                     { "ApplicationBank", "/Id" },
                     { "UserAnswers", "/UserId" }
                };

            foreach (var containerDefinition in containerDefinitions)
            {
                var containerName = containerDefinition.Key;
                var partitionKeyPath = containerDefinition.Value;

                await database.DefineContainer(containerName, partitionKeyPath)
                    .CreateIfNotExistsAsync();
            }

            services.AddSingleton(cosmosClient);
        }

    }

