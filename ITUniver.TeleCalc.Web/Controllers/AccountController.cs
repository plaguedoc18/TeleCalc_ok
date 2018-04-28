using ITUniver.TeleCalc.Web.Models;
using ITUniver.TeleCalc.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ITUniver.TeleCalc.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserRepositories UserRepository { get; set; }
        public AccountController()
        {
            var connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ituniver\TeleCalc-webDB\TeleCalc_ok\ITUniver.TeleCalc.Web\App_Data\TeleCalc.mdf;Integrated Security=True";
            UserRepository = new Repositories.UserRepositories(connString);
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //проверяем модель на валидность
            if (!ModelState.IsValid)

            // если модель не прошла проверку 
            //выдаем ошибку и открываем страницу заново
            {
                ModelState.AddModelError("", "Что-то не так");
                return View(model);
            }

            //если все хорошо 
            //отправляем запрос в бд 
            var user = UserRepository.Find($"Login = N'{model.Login}' AND Password = N'{model.Password}' ")
                .FirstOrDefault();

            // проверяем есть ли в бд запись с таким логином и паролем
            if (user == null)
            {
                //если записи нет выдаем ошибку и открываем страницу заново
                ModelState.AddModelError("", "Ошибка авторизации");
                return View(model);
            }
            //если все хорошо
            // сохраняем найденого пользователя как текущего
            FormsAuthentication.SetAuthCookie(model.Login, true);
            //переходим на страницуу нашего помогатора

            return RedirectToAction("Exec", "Calc");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}