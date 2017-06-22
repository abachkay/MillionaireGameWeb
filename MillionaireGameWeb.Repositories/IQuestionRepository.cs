using MillionaireGameWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionaireGameWeb.Repositories
{
    public interface IQuestionRepository
    {
        IList<Question> GetAll();
        void Add(Question question);
        void Update(Question question);
        void Delete(Question question);
    }
}
