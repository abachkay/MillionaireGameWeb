using MillionaireGameWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionaireGameWeb.BLL
{
    public interface IMillionaireGameManager
    {
        decimal GetResult(int questionIndex, int answerIndex);
        IList<decimal> GetAllPrices();
        Question GetQuestion(int index);
        bool[] GetTwoAnswers(int questionIndex);
        void SendMessage(string to, string description);
    }
}
