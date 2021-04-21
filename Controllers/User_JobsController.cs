using jobs.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobs.Controllers
{   [Authorize]
    public class User_JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult Get_JobsByUser(string Id)
        {
            var UserId="";
            if (Id == null)
                UserId = User.Identity.GetUserId();
            else
                UserId = Id;

            var check = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(check.ToList());

        }
        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {

            var Applyjob = db.ApplyForJobs.Find(id);
            if (Applyjob is null)
                return HttpNotFound();

            return View(Applyjob);

        }
        [Authorize]
        public ActionResult GetJobsByPublisher(string Id)
        {
            var UserID = "";
            if (Id == null)
                UserID = User.Identity.GetUserId();
            else
                UserID = Id;
           

            var jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;
            var grouped = from j in jobs
                          group j by j.Job.JobTitle
                           into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };


            return View(grouped.ToList());
        }
        public ActionResult Edit(int id)
        {
            var edit_Applyjob = db.ApplyForJobs.Find(id);
            if (edit_Applyjob == null)
                return HttpNotFound();
            return View(edit_Applyjob);
        }


        [HttpPost]
        public ActionResult Edit(ApplyForJob Edid_ApplyJob)
        {

            if (ModelState.IsValid)
            {
                Edid_ApplyJob.ApplyDate = DateTime.Now;
                db.Entry(Edid_ApplyJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Get_JobsByUser");
            }
            return View(Edid_ApplyJob);
        }
        public ActionResult Delete(int id)
        {
            var Del_ApplyJob = db.ApplyForJobs.Find(id);
            if (Del_ApplyJob == null)
                return HttpNotFound();
            return View(Del_ApplyJob);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteApplyJob(int id)
        {
            var Del_ApplyJob = db.ApplyForJobs.Find(id);
            db.ApplyForJobs.Remove(Del_ApplyJob);
            db.SaveChanges();
            return RedirectToAction("Get_JobsByUser");
        }
        public ActionResult DeleteJobByPublisher(string jobtitle)
        {
            var x1 = User.Identity.GetUserId();
            var job = db.Jobs.FirstOrDefault(x => x.JobTitle == jobtitle && x.User.Id == x1);
            db.Jobs.Remove(job);
            db.SaveChanges();

            return RedirectToAction("GetJobsByPublisher");
        }
        public FileResult GetReport(string FileName)
        {
            string FullName = Path.Combine(Server.MapPath("~/Uploads/"), FileName);
            byte[] FileBytes = System.IO.File.ReadAllBytes(FullName);
            return File(FileBytes, "application/pdf");

        }
    }
}