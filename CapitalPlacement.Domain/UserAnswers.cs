using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacement.Domain
{
    public class UserAnswers : BaseEntity
    {
        public required string AppplicationBankId { get; set; }
        public required string ApplicationQuestionId { get; set; }
        public required string UserId { get; set; }
        public required string Answer { get; set; }
    }
}
