using CapitalPlacement.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacement.Domain
{
    public class ApplicationBank : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required bool IsDefaultApplication { get; set; }

        public List<ApplicationQuestions>? ApplicationQuestions { get; set; }
    }


    public class ApplicationQuestions : BaseEntity
    {
        public required string Question { get; set; }
        public string? QuestionImageURL { get; set; }
        public required string QuestionTypes { get; set; } = nameof(QuestionType.Paragraph);

        public List<QuestionDropDown>? QuestionDropDowns { get; set; }

    }

    public class QuestionDropDown : BaseEntity
    {
        public required string DropDownName { get; set; }
    }

   
}
