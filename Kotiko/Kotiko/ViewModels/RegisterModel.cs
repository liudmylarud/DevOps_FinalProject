using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kotiko.ViewModels
{
    public class RegisterModel
    {
            [Required(ErrorMessage = "Не указан номер телефона")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Не указан пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Пароль введен неверно")]
            public string ConfirmPassword { get; set; }
    }
}
