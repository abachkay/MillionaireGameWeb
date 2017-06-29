using System;
using System.Web.Mvc;
using MillionaireGameWeb.BLL;
using Ninject;
using System.Reflection;
using MillionaireGameWeb.UI.Filters;

namespace MillionaireGameWeb.UI.Controllers
{    
    [ExceptionLogger]
    public class HomeController : Controller
    {
        private IMillionaireGameManager _gameManager;        
        public HomeController()
        {            
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _gameManager = kernel.Get<IMillionaireGameManager>();            
        }
                
        [HttpGet]
        public ActionResult Login()
        {            
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["Name"] = null;
            Session["QuestionIndex"] = null;
            Session["50/50Used"] = null;
            Session["PhoneUsed"] = null;
            Session["PeopleUsed"] = null;
            return RedirectToAction("Login");
        }        
        [HttpPost]
        public ActionResult Login(string name)
        {            
            Session["Name"] = name;
            Session["QuestionIndex"] = 0;
            Session["QuestionIndex"] = 0;
            return RedirectToAction("Index");
        }        
        public ActionResult Index()
        {            
            if (Session["Name"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();                             
        }        
        public ActionResult GetGameElements()
        {
            Session["QuestionIndex"] = Session["QuestionIndex"] ?? 0;
            var questionIndex = Convert.ToInt32(Session["QuestionIndex"]);
            var question = _gameManager.GetQuestion(questionIndex);
            ViewBag.Description = question.Description;
            ViewBag.Answers = question.Answers;
            ViewBag.Prices = _gameManager.GetAllPrices();
            ViewBag.QuestionIndex = questionIndex;            
            return PartialView("GamePartial");        
        }
        public ActionResult CheckAnswer(int answerIndex)
        {
            var result = _gameManager.GetResult(Convert.ToInt32(Session["QuestionIndex"]), answerIndex);
            if (result == -1)
            {
                Session["QuestionIndex"] = Convert.ToInt32(Session["QuestionIndex"]) + 1;
            }
            else
            {
                TempData["Result"] = result;
                return RedirectToAction("Result");
            }
            return RedirectToAction("GetGameElements");
        }
        public ActionResult Result()
        {
            Session["Name"] = null;
            Session["QuestionIndex"] = null;
            Session["50/50Used"] = null;
            Session["PhoneUsed"] = null;
            Session["PeopleUsed"] = null;
            return PartialView("ResultPartial");
        }
        public ActionResult Use50_50()
        {
            var questionIndex = Convert.ToInt32(Session["QuestionIndex"]);
            TempData["HiddenAnswers"] = _gameManager.GetTwoAnswers(questionIndex);

            Session["50/50Used"] = true;
            return RedirectToAction("GetGameElements");
        }        
        public ActionResult UsePhone(string to, string description)
        {                    
            _gameManager.SendMessage(to, description);
            Session["PhoneUsed"] = true;
            return RedirectToAction("GetGameElements");            
        }
        public ActionResult UsePeople()
        {
            var stats = _gameManager.GetStats(Convert.ToInt32(Session["QuestionIndex"]));
            TempData["Statistic0"] = stats[0];
            TempData["Statistic1"] = stats[1];
            TempData["Statistic2"] = stats[2];
            TempData["Statistic3"] = stats[3];
            Session["PeopleUsed"] = true;
            return RedirectToAction("GetGameElements");
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}