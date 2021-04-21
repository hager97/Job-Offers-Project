using jobs.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobs.Controllers
{ [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Add_Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Admin(RegisterViewModel User)
        {

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = new ApplicationUser();
            user.Email = User.UserName;
            user.UserName = User.Email;
            user.UserType = "Admin";
            user.Statues = "متاح";
            var Check = userManager.Create(user, User.Password);
            if (Check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }


            return RedirectToAction("Shows_Admins");
        }

        public ActionResult Shows_Admins()
        {
            var Admins = db.Users.Where(x => x.UserType == "Admin").ToList();
            return View(Admins);
        }
        public ActionResult Complaint()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Complaint(Complaint C)
        {
            var check = db.Users.FirstOrDefault(x => x.UserName == C.Red_Email).Id;
            if (check != null)
            {
                Complaint new_complaint = new Complaint();
                new_complaint.UserId = User.Identity.GetUserId();
                new_complaint.Red_Email = C.Red_Email;

                new_complaint.Message = C.Message;

                db.Complaints.Add(new_complaint);
                db.SaveChanges();
                ViewBag.result = "تم الارسال بنجاح";
            }
            else
            {
                ViewBag.result = "لم يتم الارسال";
            }
            return View();
        }

        public ActionResult show_complaints()
        {
            var grouped = from c in db.Complaints
                          group c by c.Red_Email
                            into complain
                          select new ComplaintsViewModel
                          {
                              Red_Email = complain.Key,
                              Items = complain
                          };
            var x = grouped.ToList();
            foreach (var v in x)
            {
                foreach (var c in db.Users)
                {
                    if (v.Red_Email == c.UserName)
                        v.Valid = c.Statues;
                }
            }

            return View(x);
        }
        [HttpGet]
        public ActionResult Profile_User(string Email)
        {
            var the_user = db.Users.FirstOrDefault(x => x.UserName == Email);
            return View(the_user);
        }
        [HttpPost]
        public ActionResult Profile_User(ApplicationUser User)
        {
            var S = db.Users.FirstOrDefault(x => x.Id == User.Id);
            S.Statues = User.Statues;
            db.Entry(S).State = EntityState.Modified;
            db.SaveChanges();
            return View(S);
        }
        public ActionResult The_Users()
        {
            var user = db.Users.Where(x=>x.UserType=="ناشر"||x.UserType=="باحث");
            return View(user);
        }
    }
}