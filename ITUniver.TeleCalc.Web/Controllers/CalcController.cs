using ITUniver.TeleCalc.Core;
using ITUniver.TeleCalc.Core.Operations;
using ITUniver.TeleCalc.Web.Models;
using ITUniver.TeleCalc.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Controllers
{
    public class CalcController : Controller
    {
        private Calc action { get; set; }
        private HistoryRepositories HistoryRepository { get; set; }
        private OperationRepositories OperationRepository { get; set; }
        public CalcController()
        {
            var connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ituniver\TeleCalc-webDB\TeleCalc_ok\ITUniver.TeleCalc.Web\App_Data\TeleCalc.mdf;Integrated Security=True";
            action = new Calc();
            HistoryRepository = new HistoryRepositories(connString);
            OperationRepository = new OperationRepositories(connString);
        }
        [HttpGet]
        public ActionResult Exec()
        {
            var model = new CalcModel();
            model.OperationList = new SelectList(action.GetOpers());
            return View(model);
        }
        [HttpPost]
        public PartialViewResult Exec(CalcModel model)   //Эта модель заполняется
        {
            var result = double.NaN;
            if (action.GetOpers().Contains(model.opername))
            {
                result = action.Exec(model.opername, model.InputData);
                var operation = OperationRepository.LoadByName(model.opername);
                if (operation != null)
                {
                    var history = new HistoryItemModel()
                    {
                        Operation = operation.Id,
                        Initiator = 1,
                        Result = result,
                        Args = string.Join(";", model.InputData),
                        CalcDate = DateTime.Now,
                        Time = 15
                    };

                    HistoryRepository.Save(history);
                }
            }
            return PartialView("_Result", result);
        }
        [HttpGet]
        public ActionResult Index(string opername, double? x, double? y)
        {
            if (action.GetOpers().Contains(opername))
            {
                ViewBag.OperName = opername.ToLower();
                ViewBag.x = x;
                ViewBag.y = y;
                ViewBag.result = action.Exec(opername, new [] { x ?? 0, y ?? 0 });
            }
            else
            {
                ViewBag.Error = "Что-то пошло не так!";
            }
            return View();
        }
        public ActionResult Operations()
        {
            ViewBag.Operations = OperationRepository.Find("").Select(o => o.Name);
            return View("Ops");
        }

        public ActionResult History()
        {
            var items = HistoryRepository.Find("");

            return View(items);
}

    }
}