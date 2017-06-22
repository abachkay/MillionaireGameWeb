using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionaireGameWeb.Entities;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Reflection;

namespace MillionaireGameWeb.Repositories
{
    public class XmlQuestionRepository : IQuestionRepository, IDisposable
    {
        private XmlReader _xmlReader;
        public XmlQuestionRepository(XmlReader xmlReader)
        {
            _xmlReader = xmlReader;
        }   
        
        public IList<Question> GetAll()
        {
            var questions = new List<Question>();
            var correctAnswerIndex = 0;                                         
            while (_xmlReader.Read())
            {
                if (_xmlReader.Name == "Question" && _xmlReader.NodeType == XmlNodeType.Element)
                {                        
                    if (_xmlReader.GetAttribute("CorrectAnswerIndex") == null || _xmlReader.GetAttribute("Description") == null || 
                        _xmlReader.GetAttribute("Id") == null || _xmlReader.GetAttribute("Price") == null)
                    {
                        throw new FormatException("XML data source has invalid format");
                    }
                    questions.Add(new Question()
                    {
                        Id = Convert.ToInt32(_xmlReader.GetAttribute("Id")),
                        Price = Convert.ToInt32(_xmlReader.GetAttribute("Price")),
                        Description = _xmlReader.GetAttribute("Description"),
                        Answers = new List<string>()                                                
                    });
                    correctAnswerIndex = Convert.ToInt32(_xmlReader.GetAttribute("CorrectAnswerIndex"));
                }
                if (_xmlReader.Name == "Answer" && _xmlReader.NodeType == XmlNodeType.Element && questions.Any())
                {
                    _xmlReader.Read();
                    if (_xmlReader.NodeType != XmlNodeType.Text)
                    {
                        throw new FormatException("XML data source has invalid format");
                    }
                    questions.Last().Answers.Add(_xmlReader.Value);
                }
                if (questions.Any() && questions.Last().Answers.Count == 4)
                {
                    questions.Last().CorrectAnswer = questions.Last().Answers.ElementAt(correctAnswerIndex);
                }
            }             
            return questions;
        }

#region For admin page (not implemented)
        public void Add(Question question)
        {
            throw new NotImplementedException();
        }

        public void Update(Question question)
        {
            throw new NotImplementedException();
        }

        public void Delete(Question question)
        {
            throw new NotImplementedException();
        }
#endregion

#region IDisposable implementation

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _xmlReader.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }       

        #endregion

    }
}
