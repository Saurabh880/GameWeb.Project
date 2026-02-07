using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Name can't be blank")]
        public string PersonName { get; set; }

        [RegularExpression("^[0-9]*$",ErrorMessage ="Phone number should contain numbers only")]
        [Required(ErrorMessage = "Phone number can't be blank")]
        public string Phone { get; set; }
        
        [EmailAddress(ErrorMessage ="Email should be in a proper Email address format")]
        [Required(ErrorMessage = "Email can't be blank")]
        public string Email { get; set; }

        [StringLength(10,ErrorMessage ="Password should be of 10 Length")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }

        [StringLength(10)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confim Password can't be blank")]
        [Compare("Password",ErrorMessage ="Password and Confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
