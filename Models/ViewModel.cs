using System.Collections.Generic;
namespace ExamOne.Models
{
    public class ViewModel
    {
        public User User {get;set;}
        public LoginUser LoginUser {get;set;}
        public List<Idea> Ideas {get;set;}
        public Idea Idea {get;set;}
        public List<Like> Likes {get;set;}
        public Like Like {get;set;}
    }
}