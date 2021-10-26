using SiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiraApi.ModelsView
{
    public class EnrollView
    {
        public List<STUDENT> Students { get; set; }
        public List<COURSE> Courses { get; set; }
    }
}