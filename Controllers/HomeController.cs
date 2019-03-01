using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExamOne.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
namespace ExamOne.Controllers
{
    public class HomeController : Controller
    {
        private Context _dbContext;
        public HomeController(Context context)
        {
            _dbContext = context;
        }
        //==================[LOGIN REG PAGE]======================
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        //==============================[LOGIN]==========================
        [HttpPost("login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                User userInDb = _dbContext.Users.FirstOrDefault(u => u.Email == loginUser.Email);
                if (userInDb == null)
                {
                    ModelState.AddModelError("LoginUser.Email", "Invalid Email/Password");
                    return View("Index");
                }
                PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                if (Hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.Password) == 0)
                {
                    ModelState.AddModelError("LoginUser.Email", "Invalid Email/Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("ID", userInDb.UserId);
                return RedirectToAction("Ideas");
            }
            return View("Index");
        }
        //=======================[REGISTER USER]=============================
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("User.Email", "Email already exists.");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _dbContext.Add(user);
                _dbContext.SaveChanges();
                HttpContext.Session.SetInt32("ID", user.UserId);
                return RedirectToAction("Ideas");
            }
            return View("Index");
        }
        //========================[LOGOUT USER]============================
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        //================[IDEAS PAGE]============================
        [HttpGet("/ideas")]
        public IActionResult Ideas()
        {
            if (HttpContext.Session.GetInt32("ID") == null)
            {
                return RedirectToAction("Index");
            }
            ViewModel ideas = new ViewModel();
            int id = HttpContext.Session.GetInt32("ID") ?? default(int);
            ViewBag.CurrentUser = _dbContext.Users.FirstOrDefault(u =>u.UserId == id);
            ideas.Ideas = _dbContext.Ideas.Include(i => i.Likes).Include(i => i.user).OrderByDescending(i => i.Likes.Count).ToList();
            ideas.User = _dbContext.Users.FirstOrDefault(u => u.UserId == id);
            return View(ideas);
        }
        //====================[NEW IDEA]=========================
        [HttpPost("new/idea")]
        public IActionResult NewIdea(Idea idea)
        {
            if (ModelState.IsValid)
            {
                idea.UserId = HttpContext.Session.GetInt32("ID") ?? default(int);
                _dbContext.Ideas.Add(idea);
                _dbContext.SaveChanges();
                return RedirectToAction("Ideas");
            }
           
            ViewModel ideas = new ViewModel();
            int id = HttpContext.Session.GetInt32("ID") ?? default(int);
            ideas.User = _dbContext.Users.FirstOrDefault(u => u.UserId == id);
            ideas.Ideas = _dbContext.Ideas.Include(i => i.Likes).Include(i => i.user).OrderByDescending(i => i.Likes.Count).ToList();
            return View("Ideas",ideas);
        }
        //==========================[LIKE AN IDEA]==========================
        [HttpGet("like/{ideaId}")]
        public IActionResult Like(int ideaId, Like like)
        {
            if (HttpContext.Session.GetInt32("ID") == null)
            {
                return RedirectToAction("Index");
            }
            int id = HttpContext.Session.GetInt32("ID") ?? default(int);
            if (_dbContext.Likes.Any(l => l.UserId == id && l.IdeaId == ideaId))
            {
                return RedirectToAction("Ideas");
            }
            like.UserId = id;
            like.IdeaId = ideaId;
            _dbContext.Likes.Add(like);
            _dbContext.SaveChanges();
            return RedirectToAction("Ideas");
        }
        //=====================[SHOW IDEA]===============================
        [HttpGet("bright_idea/{ideaId}")]
        public IActionResult ShowIdea(int ideaId)
        {
            if (HttpContext.Session.GetInt32("ID") == null)
            {
                return RedirectToAction("Index");
            }
            ViewModel idea = new ViewModel();
            idea.Idea = _dbContext.Ideas.FirstOrDefault(i => i.IdeaId == ideaId);
            idea.User = _dbContext.Users.FirstOrDefault(u => u.UserId == idea.Idea.UserId);
            
            ViewBag.Likes = _dbContext.Likes.Where(l => l.IdeaId == ideaId).Include(i => i.User);
            return View(idea);
        }
        //==================[SHOW USER]===================================
        [HttpGet("users/{userId}")]
        public IActionResult ShowUser(int userId)
        {
            if(HttpContext.Session.GetInt32("ID") == null)
            {
                return RedirectToAction("Index");
            }
            ViewModel person = new ViewModel();
            person.User = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            ViewBag.Ideas = _dbContext.Ideas.Where(i => i.UserId == userId).Count();
            ViewBag.Likes = _dbContext.Likes.Where(l => l.UserId == userId).Count();
            return View(person);
        }
        //=========================[REMOVE IDEA]=================================
        [HttpGet("delete/{ideaId}")]
        public IActionResult DeleteIdea(int ideaId)
        {
            if (HttpContext.Session.GetInt32("ID") == null)
            {
                return RedirectToAction("Index");
            }
            Idea idea = _dbContext.Ideas.FirstOrDefault(i => i.IdeaId == ideaId);
            if(idea.UserId != HttpContext.Session.GetInt32("ID"))
            {
                return RedirectToAction("Ideas");
            }
            _dbContext.Ideas.Remove(idea);
            _dbContext.SaveChanges();
            return RedirectToAction("Ideas");
        }
    }
}
