using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace jobs.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string UserId { get; set; }
         [Required]
         [Display(Name ="ايميل المستخدم المقدم اليه الشكوي")]
        public string Red_Email { get; set; }
        
        [Required]
        [AllowHtml]
        public string Message { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}