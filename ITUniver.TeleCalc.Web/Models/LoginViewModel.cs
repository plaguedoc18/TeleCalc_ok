using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Да кто ты такой") ]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}