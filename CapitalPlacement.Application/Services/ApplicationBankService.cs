


namespace CapitalPlacement.Application.Services;

public class ApplicationBankService : IApplicationBankService
{
    private readonly IQuestionRepository questionRepository;
    public ApplicationBankService(IQuestionRepository _questionRepository)
    {
        questionRepository = _questionRepository;
    }

    public async Task<StandardResponse<ApplicationBank>> AddNewApplication(ApplicationBank applicationBank)
    {
        var response = await questionRepository.AddNewApplication(applicationBank);
        if (response is null) return StandardResponse<ApplicationBank>.Failed("Unable to add Application", response);

        return StandardResponse<ApplicationBank>.Succeeded("New Application was addedd successfully", response);
    }

    public async Task<StandardResponse<ApplicationBank>> AddNewApplicationQuestions(ApplicationQuestions applicationQuestions, string ApplicationBankId)
    {
        var response = await questionRepository.AddNewApplicationQuestions(applicationQuestions, ApplicationBankId);
        if (response is null) return StandardResponse<ApplicationBank>.Failed("Unable to add question", response);
        return StandardResponse<ApplicationBank>.Succeeded("New question was addedd successfully", response);
    }

    public async Task<StandardResponse<IEnumerable<ApplicationBank>>> GetAllApplicationQuestion()
    {
        var response = await questionRepository.GetAllQuestionsAsync();
        if (response is null) return StandardResponse<IEnumerable<ApplicationBank>>.Failed("Unable to all application", response);
        return StandardResponse<IEnumerable<ApplicationBank>>.Succeeded("List of Application", response);
    }

    public async Task<StandardResponse<ApplicationBank>> GetApplicationByIdAsync(string appId)
    {
        var response = await questionRepository.GetApplicationByIdAsync(appId);
        if (response is null) return StandardResponse<ApplicationBank>.Failed("Unable to get application by id", response);
        return StandardResponse<ApplicationBank>.Succeeded("List of Application By ID", response);
    }

    public async Task<StandardResponse<ApplicationBank>> UpdateApplicationBankAsync(ApplicationBank ApplicationBank, string ApplicationId)
    {
        var getApplication = await questionRepository.GetApplicationByIdAsync(ApplicationId);
        if (getApplication is null)
        {
            return null;
        }
        getApplication.Name = ApplicationBank.Name;
        getApplication.Description = ApplicationBank.Description;
        getApplication.UpdatedDate = DateTime.Now;
        var response = await questionRepository.UpdateApplicationBankAsync(getApplication);
        if (response is null) return StandardResponse<ApplicationBank>.Failed("Unable to update Application", response);
        return StandardResponse<ApplicationBank>.Succeeded("Application was updated successfully", response);
       
    }

    public async Task<StandardResponse<ApplicationBank>> UpdateApplicationQuestionAsync(ApplicationQuestions ApplicationBank, string ApplicationId, string QuestionId)
    {
        var getApplication = await questionRepository.GetApplicationByIdAsync(ApplicationId);
        if (getApplication is null)
        {
            return null;
        }

        if (getApplication.ApplicationQuestions is null) return null;

        foreach (var item in getApplication.ApplicationQuestions)
        {
            if (item.Id.ToString() == QuestionId)
            {
                item.Question = ApplicationBank.Question;
                item.QuestionImageURL = ApplicationBank.QuestionImageURL;
                item.QuestionTypes = ApplicationBank.QuestionTypes;
            }
        }
        getApplication.UpdatedDate = DateTime.Now;
        var response = await questionRepository.UpdateApplicationBankAsync(getApplication);
        if (response is null) return StandardResponse<ApplicationBank>.Failed("Unable to update question bank", response);
        return StandardResponse<ApplicationBank>.Succeeded("Question was updated successfully", response);
    }

}
