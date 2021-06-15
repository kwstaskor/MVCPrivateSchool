using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSchool.Models.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
    }
}