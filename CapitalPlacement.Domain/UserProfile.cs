
namespace CapitalPlacement.Domain
{
    public class UserProfile : BaseEntity
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set;}
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IDNumber { get; set; }
        public required DateTime DateofBirth { get; set; }
        public required string Gender { get; set; }
    }
}
