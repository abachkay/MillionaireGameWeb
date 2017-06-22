using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionaireGameWeb.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ICollection<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
