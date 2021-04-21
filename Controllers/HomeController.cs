using jobs.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace jobs.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Details(int id)
        {
            var job = db.Jobs.Find(id);
            if (job is null)
                return HttpNotFound();
            Session["JobId"] = id;
            return View(job);

        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(ApplyForJob job, HttpPostedFileBase CV)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), CV.FileName);
                CV.SaveAs(path);
                //var job = new ApplyForJob();
                job.JobId = JobId;
                job.UserId = UserId;
                job.Message = job.Message;
                job.CV = CV.FileName;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.result = "تم الاضافة بنجاح";
            }
            else
            {
                ViewBag.result = "المعذرة ,لقد سبق و تقدمت الي  الوظيفة";
            }


            return View();
        }
 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var logoinfo = new NetworkCredential("hagerhussien55@gmail.com", "Hungergame@97");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("hagerhussien55@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل :" + contact.Name + "<hr/>" +

                         "نص الرسال :<br/>" + contact.Message + "<br/>";
            mail.Body = body;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = logoinfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchname)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchname) ||
                                          a.JobContent.Contains(searchname) ||
                                          a.Category.CategoryName.Contains(searchname) ||
                                          a.Category.CategoryDescription.Contains(searchname)).ToList();

            return View(result);

        }
        
       
        
      

        
    }


}