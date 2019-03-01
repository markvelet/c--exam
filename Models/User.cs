using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ExamOne.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required(ErrorMessage="First Name Required")]
        [MinLength(3, ErrorMessage="First Name too short")]
        [MaxLength(255)]
        [Display(Name="First Name:")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage="Only characters allowed")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="Last Name Required")]
        [MinLength(3, ErrorMessage="Last Name too short")]
        [MaxLength(255)]
        [Display(Name="Last Name:")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage="Only characters allowed")]
        public string LastName {get;set;}

        [Required(ErrorMessage="Email Required")]
        [EmailAddress]
        [Display(Name="Email:")]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Email invalid")]
        public string Email {get;set;}

        [Required(ErrorMessage="Password Required")]
        [Display(Name="Password 8 characters min")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [NotMapped]
        [Required(ErrorMessage="Please confirm password")]
        [Compare("Password", ErrorMessage="Password do not match")]
        [DataType(DataType.Password)][Display(Name="Confirm Password")]
        public string Confirm {get;set;}

        [Required(ErrorMessage="Alias Required")]
        [MinLength(3, ErrorMessage="Alias too short")]
        [MaxLength(255)]
        [Display(Name="Alias:")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage="Only characters allowed")]
        public string alias {get;set;}

        [NotMapped]
        public List<Like> Likes {get;set;}
        public List<Idea> Ideas {get;set;}
        public User()
        {
            Likes = new List<Like>();
        }

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}