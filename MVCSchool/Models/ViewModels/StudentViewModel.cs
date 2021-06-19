using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCSchool.UnitOfWork;
namespace MVCSchool.Models.ViewModels
{
    public class StudentViewModel
    {
        private readonly IUnitOfWork unitOfWork;

        public Student Student { get; set; }
        public MultiSelectList CourseList
        {
            get
            {
                var courses = unitOfWork.Courses.Get();
                return new MultiSelectList(courses, "CourseId", "Title");

            }
        }

        public MultiSelectList AssignmentList
        {
            get
            {
                var assignments = unitOfWork.Assignments.Get();
                return new MultiSelectList(assignments, "AssignmentId", "Title");
            }
        }

        public IEnumerable<SelectListItem> CourseEdit
        {
            get
            {
                var studentCoursesIds = Student.Courses.Select(course => course.CourseId);
                return unitOfWork.Courses.Get().ToList().Select(c => new SelectListItem()
                {
                    Value = c.CourseId.ToString(),
                    Text = string.Format($"{c.Title} {c.Stream}"),
                    Selected = studentCoursesIds.Any(selected => selected == c.CourseId)
                });
            }
        }

        public IEnumerable<SelectListItem> assignmentEdit
        {
            get
            {
                var studentCoursesIds = Student.Assignments.Select(assignment => assignment.AssignmentId);
                return unitOfWork.Assignments.Get().ToList().Select(a => new SelectListItem()
                {
                    Value = a.AssignmentId.ToString(),
                    Text = string.Format($"{a.Title} - {a.Description}"),
                    Selected = studentCoursesIds.Any(selected => selected == a.AssignmentId)
                });
            }
        }


        public StudentViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public StudentViewModel(IUnitOfWork unitOfWork, Student student)
        {
            this.unitOfWork = unitOfWork;
            Student = student;
        }


    }
}