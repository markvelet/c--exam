using System;
using System.ComponentModel.DataAnnotations;
namespace ExamOne.Models
{
    public class Like
    {
        [Key]
        public int LikeId {get;set;}
        public int IdeaId {get;set;}
        public int UserId {get;set;}
        public Idea Idea {get;set;}
        public User User {get;set;}
    }
}