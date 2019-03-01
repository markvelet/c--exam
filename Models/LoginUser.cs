using System.ComponentModel.DataAnnotations;
namespace ExamOne.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage="Email Required")]
        [EmailAddress]
        [Display(Name="Email:")]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Invalid Email/Password")]
        public string Email {get;set;}

        [Required(ErrorMessage="Password Required")]
        [Display(Name="Password:")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }
}