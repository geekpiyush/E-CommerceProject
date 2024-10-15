using Entities.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class RegisterDTO
    {

        [Required(ErrorMessage ="Customer Name Can't be Blank")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email Can't be Blank")]
        [EmailAddress(ErrorMessage ="Email Should be in a proper email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Can't be Blank")]
        [RegularExpression("^[0-9]*$",ErrorMessage ="Phone Number Should Contains Only Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password Can't be Blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Can't be Blank")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public UserTypeOptions UserType {  get; set; } = UserTypeOptions.Customer;
       

    }
}
