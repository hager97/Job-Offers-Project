using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobs.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        [AllowHtml]////علشان ckieditor
        public string Message { get; set; }
        public string CV { get; set; }

        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public virtual Job Job { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}