using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobs.Models
{
    public class ComplaintsViewModel
    {  
        public string Red_Email { get; set; }
        public string Valid { get; set; }
        public IGrouping<string, Complaint> Items { get; set; }
    }
}