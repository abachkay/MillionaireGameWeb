using System.ComponentModel.DataAnnotations;

namespace MillionaireGameWeb.Entities
{
    public class UserAnswerLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int QuestionNumber { get; set; }
        [Required]
        public int FirstAnswerCount { get; set; }
        [Required]
        public int SecondAnswerCount { get; set; }
        [Required]
        public int ThirdAnswerCount { get; set; }
        [Required]
        public int FourthAnswerCount { get; set; }
    }
}
