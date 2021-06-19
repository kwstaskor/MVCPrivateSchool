using System.Collections.Generic;
using PagedList;

namespace MVCSchool.Models.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
        
        public IPagedList<Course> PagedCourses { get; set; }
        public IPagedList<Assignment> PagedAssignments { get; set; }
        public IPagedList<Student> PagedStudents { get; set; }
        public IPagedList<Trainer> PagedTrainers { get; set; }
    }
}