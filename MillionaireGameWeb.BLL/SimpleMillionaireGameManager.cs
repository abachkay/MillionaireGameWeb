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
            // Logging
            using (var context = new LoggerContext())
            {
                var log = context.UserAnswerLogs.Where(q => q.QuestionNumber == questionIndex).FirstOrDefault();
                if (log == null)
                {
                    log = new UserAnswerLog() { QuestionNumber = questionIndex, FirstAnswerCount = 0, SecondAnswerCount = 0, ThirdAnswerCount = 0, FourthAnswerCount = 0 };
                    log = context.UserAnswerLogs.Add(log);
                    context.SaveChanges();
                }
                if(answerIndex == 0)
                {
                    log.FirstAnswerCount++;
                }
                if (answerIndex == 1)
                {
                    log.SecondAnswerCount++;
                }
                if (answerIndex == 2)
                {
                    log.ThirdAnswerCount++;
                }
                if (answerIndex == 3)
                {
                    log.FourthAnswerCount++;
                }
                context.SaveChanges();
            }
            using (var xmlReader = XmlReader.Create(_xmlUrl))
            {
                var questions = new XmlQuestionRepository(xmlReader).GetAll();
                if (questions[questionIndex].CorrectAnswer == questions[questionIndex].Answers.ElementAt(answerIndex))
                {
                    if (questionIndex != questions.Count - 1)
                    {
                        // Game still on
                        return -1;
                    }
                    else
                    {
                        // Player wins
                        return questions[questionIndex].Price;
                    }
                }
                else
                {
                    // Calculating prize as first guaranteed question
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
                var result = new bool[4] { true, true, true, true };
                var questions = new XmlQuestionRepository(xmlReader).GetAll();
                var correctAnswerIndex = questions[questionIndex].Answers.ToList().IndexOf(questions[questionIndex].CorrectAnswer);
                result[correctAnswerIndex] = false;

                using (var context = new LoggerContext())
                {
                    var log = context.UserAnswerLogs.Where(l => l.QuestionNumber == questionIndex).FirstOrDefault();
                    if (log == null)
                    {
                        var rand = new Random();
                        int i = rand.Next(4);
                        while (i == correctAnswerIndex)
                        {
                            i = rand.Next(4);
                        }
                        result[i] = false;
                    }
                    else
                    {
                        var stats = new int[] { log.FirstAnswerCount, log.SecondAnswerCount, log.ThirdAnswerCount, log.FourthAnswerCount, };
                        int indexOfMax = 0;
                        int max = -1;
                        for (int i = 0; i < 4; i++)
                        {
                            if (stats[i] >= max && i != correctAnswerIndex)
                            {
                                indexOfMax = i;
                                max = stats[i];
                            }
                        }
                        result[indexOfMax] = false;
                    }
                }
                return result;
            }
        }

        public int[] GetStats(int questionIndex)
        {
            using (var context = new LoggerContext())
            {
                var log = context.UserAnswerLogs.Where(l => l.QuestionNumber == questionIndex).FirstOrDefault();
                if (log == null)
                {
                    return new int[] { 25,25,25,25 };
                }
                else
                {
                    var stats = new int[] { log.FirstAnswerCount, log.SecondAnswerCount, log.ThirdAnswerCount, log.FourthAnswerCount, };
                    var sum = stats.Sum();
                    for(int i=0;i<4;i++)
                    {
                        stats[i] = stats[i] * 100 / sum;
                    }
                    if (stats.Sum() == 99)
                    {
                        stats[3]++;
                    }
                    return stats;
                }
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
