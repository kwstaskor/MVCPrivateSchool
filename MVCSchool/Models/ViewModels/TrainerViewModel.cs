using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Models.ViewModels
{
    public class TrainerViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        public Trainer Trainer { get; set; }

        public MultiSelectList CourseList
        {
            get
            {
                return new MultiSelectList(unitOfWork.Courses.Get(), "CourseId", "Title");
            }
        }

        public IEnumerable<SelectListItem> CourseEdit
        {
            get
            {
                unitOfWork.Trainers.AttachTrainerCourses(Trainer);
                var trainerCoursesIds = Trainer.Courses.Select(course => course.CourseId);
                return unitOfWork.Courses.Get().Select(c => new SelectListItem()
                {
                    Value = c.CourseId.ToString(),
                    Text = string.Format($"{c.Title} {c.Stream}"),
                    Selected = trainerCoursesIds.Any(selected => selected == c.CourseId)
                });
            }
        }

        public TrainerViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public TrainerViewModel(IUnitOfWork unitOfWork ,Trainer trainer)
        {
            this.unitOfWork = unitOfWork;
            Trainer = trainer;
        }
    }
}