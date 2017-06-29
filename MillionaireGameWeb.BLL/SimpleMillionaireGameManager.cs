using MillionaireGameWeb.Entities;
using MillionaireGameWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MillionaireGameWeb.BLL
{
    public class SimpleMillionaireGameManager: IMillionaireGameManager
    {
        private string _xmlUrl;
        public SimpleMillionaireGameManager(string xmlUrl)
        {
            _xmlUrl = xmlUrl;
        }
        public int CurrentQuestionIndex => throw new NotImplementedException();

        private decimal CalculateResult(int index)
        {
            using (var xmlReader = XmlReader.Create(_xmlUrl))
            {
                var questions = new XmlQuestionRepository(xmlReader).GetAll();                
                while(true)
                {
                    index--;
                    if (index == 12 || index == 8 || index == 4)
                    {
                        return questions[index].Price;
                    }
                    if (index == -1)
                    {
                        return 0;
                    }
                }
            }
        }

        public decimal GetResult(int questionIndex, int answerIndex)
        {
            using (var xmlReader = XmlReader.Create(_xmlUrl))
            {
                var questions = new XmlQuestionRepository(xmlReader).GetAll();
                if (questions[questionIndex].CorrectAnswer == questions[questionIndex].Answers.ElementAt(answerIndex))
                {
                    if (questionIndex != questions.Count - 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return questions[questionIndex].Price;
                    }
                }
                else
                {
                    return CalculateResult(questionIndex);
                }
            }
        }

        public IList<decimal> GetAllPrices()
        {
            using (var xmlReader = XmlReader.Create(_xmlUrl))
            {
                return new XmlQuestionRepository(xmlReader).GetAll().Select(q=> q.Price).ToList();
            }
        }

        public Question GetQuestion(int index)
        {
            using (var xmlReader = XmlReader.Create(_xmlUrl))
            {
                var questions = new XmlQuestionRepository(xmlReader).GetAll();
                if (index>questions.Count-1)
                {
                    throw new IndexOutOfRangeException();
                }
                return questions[index];
            }
        }

        public bool[] GetTwoAnswers(int questionIndex)
        {          
            using (var xmlReader = XmlReader.Create(_xmlUrl))
            {
                var result = new bool[4];
                var questions = new XmlQuestionRepository(xmlReader).GetAll();
                var correctAnswerIndex = questions[questionIndex].Answers.ToList().IndexOf(questions[questionIndex].CorrectAnswer);
                result[correctAnswerIndex] = false;
                var rand = new Random();
                int i1 = rand.Next(4), i2= rand.Next(4);
                while (i1 == correctAnswerIndex)
                {
                    i1 = rand.Next(4);
                }
                while (i2 == correctAnswerIndex || i2 == i1)
                {
                    i2 = rand.Next(4);
                }
                result[i1] = true;
                result[i2] = true;
                return result;
            }
        }

        public void SendMessage(string to,string description)
        {  
            var from = "abachkayspare@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Millionaire game";
            message.Body = $"Your friend is playing millionaire game, help him with question:\n{description}";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("abachkayspare@gmail.com", "abachkay142");
            client.Send(message);      
        }
    }
}
