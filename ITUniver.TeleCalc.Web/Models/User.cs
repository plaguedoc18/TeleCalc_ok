using ITUniver.TeleCalc.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// True - мужской False - женский
        /// </summary>
        public bool Sex { get; set; }
        public int Status { get; set; }
    }
}