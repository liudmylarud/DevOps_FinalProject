using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kotiko.ViewModels
{
    public class RegisterModel
    {
            [Required(ErrorMessage = "Phone number not specified")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Password not specified")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password entered incorrectly")]
            public string ConfirmPassword { get; set; }
    }
}
