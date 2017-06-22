using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MillionaireGameWeb.Repositories;
using System.Xml;
using MillionaireGameWeb.Entities;
using MillionaireGameWeb.BLL;
using Ninject;
using System.Reflection;

namespace MillionaireGameWeb.UI.Controllers
{       
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
            if (_gameManager.GetResult(Convert.ToInt32(Session["QuestionIndex"]), answerIndex) == -1)
            {
                Session["QuestionIndex"] = Convert.ToInt32(Session["QuestionIndex"]) + 1;
            }
            else
            {
                TempData["Result"] = _gameManager.GetResult(Convert.ToInt32(Session["QuestionIndex"]), answerIndex);
            }
            return RedirectToAction("GetGameElements");
        }
        public ActionResult Use50_50()
        {
            var questionIndex = Convert.ToInt32(Session["QuestionIndex"]);
            TempData["HiddenAnswers"] = _gameManager.GetTwoAnswers(questionIndex);

            Session["50/50Used"] = true;
            return RedirectToAction("GetGameElements");
        }
        public ActionResult UsePhone(string from, string to, string description)
        {
            _gameManager.SendMessage(from, to, description);
            Session["PhoneUsed"] = true;
            return RedirectToAction("GetGameElements");
        }
        public ActionResult UsePeople()
        {
            Session["PeopleUsed"] = true;
            return RedirectToAction("GetGameElements");
        }
    }
}