using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class AppController: Controller
    {
        private readonly IMailService _mailService;
        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            //throw new InvalidOperationException();
            ViewBag.Title = "Home Page";
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
          //  ViewBag.Title = "Contact Us";
          // throw new InvalidOperationException("Bad Things happen");

           return View();
        }
         [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            //ViewBag.Title = "Contact Us";
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("10ashmeet@gmail.com", model.Subject, $"From:{model.Name}-{model.Email},Message:{model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
           
            return View();

        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
    }
}
