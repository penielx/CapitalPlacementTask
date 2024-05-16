using CapitalPlacement.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacement.Shared.Dtos
{
    public class ApplicationBankDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required bool IsDefaultApplication { get; set; }

        public List<ApplicationQuestionsDto>? ApplicationQuestions { get; set; }
    }

    public class ApplicationQuestionsDto
    {
        public required string Question { get; set; }
        public string? QuestionImageURL { get; set; } = "No Image";
        public required string QuestionTypes { get; set; } = nameof(QuestionType.Paragraph);

        public List<QuestionDropDownDto>? QuestionDropDowns { get; set; }

    }

    public class QuestionDropDownDto
    {
        public required string DropDownName { get; set; }
    }


}
