using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ExamOne.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId {get;set;}
        public int UserId {get;set;}
        public User user {get;set;}
        [Required(ErrorMessage="Idea required")]
        [MinLength(5, ErrorMessage="Idea too short")]
        [MaxLength(255, ErrorMessage="Idea too long, max 255 characters")]
        [Display(Name="Your bright idea:")]
        public string Content {get;set;}
        public List<Like> Likes {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}