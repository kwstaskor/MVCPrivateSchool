using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Models.ViewModels
{
    public class AssignmentViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        public Assignment Assignment { get; set; }

        public MultiSelectList CourseList
        {
            get
            {
                return new MultiSelectList(unitOfWork.Courses.Get(), "CourseId", "Title");
            }
        }

        public MultiSelectList StudentList
        {
            get
            {
                return new MultiSelectList(unitOfWork.Students.Get().Select(s=>new SelectListItem()
                {
                    Value = s.StudentId.ToString(),
                    Text = string.Format($"{s.FirstName} {s.LastName}")
                }), "Value", "Text");
            }
        }

        public IEnumerable<SelectListItem> CourseEdit
        {
            get
            {
                unitOfWork.Assignments.AttachAssignmentCourses(Assignment);
                var assignmentCoursesIds = Assignment.Courses.Select(course => course.CourseId);
                return unitOfWork.Courses.Get().ToList().Select(c => new SelectListItem()
                {
                    Value = c.CourseId.ToString(),
                    Text = string.Format($"{c.Title} {c.Stream}"),
                    Selected = assignmentCoursesIds.Any(selected => selected == c.CourseId)
                });
            }
        }

        public IEnumerable<SelectListItem> StudentEdit
        {
            get
            {
                unitOfWork.Assignments.AttachAssignmentStudents(Assignment);
                var assignmentStudentsIds = Assignment.Students.Select(student => student.StudentId);
                return unitOfWork.Students.Get().ToList().Select(s => new SelectListItem()
                {
                    Value = s.StudentId.ToString(),
                    Text = string.Format($"{s.FirstName} {s.LastName}"),
                    Selected = assignmentStudentsIds.Any(selected => selected == s.StudentId)
                });
            }
        }

        public AssignmentViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public AssignmentViewModel(IUnitOfWork unitOfWork, Assignment assignment)
        {
            this.unitOfWork = unitOfWork;
            Assignment = assignment;
        }
    }
}