using DutchTreat.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AppController: Controller
    {
        private readonly IMailService _mailService;
        private readonly DutchContext _ctx;
        private readonly IDutchRepository _repository;

        public AppController(IMailService mailService,DutchContext ctx,IDutchRepository repository)
        {
            _mailService = mailService;
            _ctx = ctx;
            _repository = repository;
        }
        public IActionResult Index()
        {
            //throw new InvalidOperationException();
            // ViewBag.Title = "Home Page";
            var results = _ctx.Products.ToList();
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
        [Authorize]
        public IActionResult Shop()
        {
            //var result = _context.Products
            //  .OrderBy(p =>p.Category)
            //  .ToList();

            //var results = from p in _ctx.Products
            //            orderby p.Category
            //select p;
            //return View(results.ToList());

            // var result = _repository.GetAllProducts();
            //return View(result);
            return View();
        }
    }
}
