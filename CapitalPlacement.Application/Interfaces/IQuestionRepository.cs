using CapitalPlacement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacement.Application.Interfaces
{
    public interface IQuestionRepository
    {
        Task<ApplicationBank> AddNewApplication(ApplicationBank applicationBank);
        Task<ApplicationBank> AddNewApplicationQuestions(ApplicationQuestions applicationQuestions, string ApplicationBankId);
        Task<IEnumerable<ApplicationBank>> GetAllQuestionsAsync();
        Task<ApplicationBank> GetApplicationByIdAsync(string applicationId);
        Task<ApplicationBank> GetApplicationByIsDefault(bool IsDefault);
        Task<ApplicationBank> UpdateApplicationBankAsync(ApplicationBank task);
        Task DeleteApplicationbankAsync(string appId, string name);
    }
}
