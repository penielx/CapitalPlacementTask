using CapitalPlacement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacement.Application.Interfaces.Application_Layer
{
    public interface IApplicationBankService
    {
        Task<StandardResponse<ApplicationBank>> AddNewApplication(ApplicationBank applicationBank);

        Task<StandardResponse<ApplicationBank>> AddNewApplicationQuestions(ApplicationQuestions applicationQuestions, string ApplicationBankId);

        Task<StandardResponse<IEnumerable<ApplicationBank>>> GetAllApplicationQuestion();

        Task<StandardResponse<ApplicationBank>> GetApplicationByIdAsync(string appId);

        Task<StandardResponse<ApplicationBank>> UpdateApplicationBankAsync(ApplicationBank ApplicationBank, string ApplicationId);

        Task<StandardResponse<ApplicationBank>> UpdateApplicationQuestionAsync(ApplicationQuestions ApplicationBank, string ApplicationId, string QuestionId);


    }
}
