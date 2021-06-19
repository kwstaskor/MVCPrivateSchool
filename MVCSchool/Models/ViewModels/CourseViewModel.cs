using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Models.ViewModels
{
    public class CourseViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        public Course Course { get; set; }

        public MultiSelectList StudentList
        {
            get
            {
                return new MultiSelectList(unitOfWork.Students.Get().ToList().Select(s => new SelectListItem()
                {
                    Value = s.StudentId.ToString(),
                    Text = string.Format($"{s.FirstName} {s.LastName}")
                }), "Value", "Text");
            }
        }

        public MultiSelectList TrainerList
        {
            get
            {
                return new MultiSelectList(unitOfWork.Trainers.Get().ToList().Select(t => new SelectListItem()
                {
                    Value = t.TrainerId.ToString(),
                    Text = string.Format($"{t.FirstName} {t.LastName}")
                }), "Value", "Text");
            }
        }

        public MultiSelectList AssignmentList
        {
            get
            {
                return new MultiSelectList(unitOfWork.Assignments.Get(), "AssignmentId", "Title");
            }
        }

        public IEnumerable<SelectListItem> StudentEdit
        {
            get
            {
                unitOfWork.Courses.AttachStudentsCourse(Course);
                var courseStudentsIds = Course.Students.Select(student => student.StudentId);
                return unitOfWork.Students.Get().ToList().Select(s => new SelectListItem()
                {
                    Value = s.StudentId.ToString(),
                    Text = string.Format($"{s.FirstName} {s.LastName}"),
                    Selected = courseStudentsIds.Any(selected => selected == s.StudentId)
                });
            }
        }

        public IEnumerable<SelectListItem> TrainerEdit
        {
            get
            {
                unitOfWork.Courses.AttachTrainersCourse(Course);
                var courseTrainersIds = Course.Trainers.Select(trainer => trainer.TrainerId);
                return unitOfWork.Trainers.Get().ToList().Select(t => new SelectListItem()
                {
                    Value = t.TrainerId.ToString(),
                    Text = string.Format($"{t.FirstName} {t.LastName}"),
                    Selected = courseTrainersIds.Any(selected => selected == t.TrainerId)
                });
            }
        }

        public IEnumerable<SelectListItem> AssignmentEdit
        {
            get
            {
                unitOfWork.Courses.AttachAssignmentsCourse(Course);
                var courseAssignmentsIds = Course.Assignments.Select(assignment => assignment.AssignmentId);
                return unitOfWork.Assignments.Get().ToList().Select(a => new SelectListItem()
                {
                    Value = a.AssignmentId.ToString(),
                    Text = string.Format($"{a.Title} - {a.Description}"),
                    Selected = courseAssignmentsIds.Any(selected => selected == a.AssignmentId)
                });
            }
        }

        public CourseViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public CourseViewModel(IUnitOfWork unitOfWork , Course course)
        {
            this.unitOfWork = unitOfWork;
            Course = course;
        }
    }
}