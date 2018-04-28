using ITUniver.TeleCalc.Web.Models;
using ITUniver.TeleCalc.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Controllers
{
    public class ManageController : Controller
    {
        private UserRepositories UserRepository { get; set; }
        public ManageController()
        {
            var connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ituniver\TeleCalc-webDB\TeleCalc_ok\ITUniver.TeleCalc.Web\App_Data\TeleCalc.mdf;Integrated Security=True";
            UserRepository = new Repositories.UserRepositories(connString);
        }
        // GET: Manage
        public ActionResult UpCreate(User NewUser)
        {
            var user = UserRepository.Find($"Id = N'{NewUser.Id}'")
                .FirstOrDefault();
            if (user == null)
            {

            }
            return View();
        }
        public ActionResult Index()
        {
            var users = UserRepository.Find("");
            return View(users);
        }
        public ActionResult Edit(int id)
        {
            var users = UserRepository.Load(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
           if( UserRepository.Save(model))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Не сохранился.");
            return View(model);
        }
    }
}