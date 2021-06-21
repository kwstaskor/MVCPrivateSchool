using System.Collections.Generic;
using MVCSchool.UnitOfWork;
using PagedList;

namespace MVCSchool.Models.ViewModels
{
    public class AdminViewModel
    {
        private IUnitOfWork unitOfWork;

        public AdminViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            Courses = unitOfWork.Courses.Get();
            Assignments = unitOfWork.Assignments.Get();
            Trainers = unitOfWork.Trainers.Get();
            Students = unitOfWork.Students.Get();
        }

        //Searching Props
        public string SearchByNameAss { get; set; }
        public string SearchByNameCrs { get; set; }
        public string SearchByNameStd { get; set; }
        public string SearchByNameTr { get; set; }

        //Filtering Props
        
        #region Course Props
        public string TitleCSort { get; set; }
        public string StreamSort { get; set; }
        public string TypeSort { get; set; }
        public string SdSort { get; set; }
        public string EdSort { get; set; }
        #endregion

        #region Assignment Props

        public string TitleASort { get; set; }
        public string DescriptionSort { get; set; }
        public string SubmissionSort { get; set; }
        public string OralSort { get; set; }
        public string TotalSort { get; set; }

        #endregion

        #region Student Props

        public string FirstNameSSort { get; set; }
        public string LastNameSSort { get; set; }
        public string DobSSort { get; set; }
        public string FeeSSort { get; set; }

        #endregion

        #region Trainer Props

        public string FirstNameTSort { get; set; }
        public string LastNameTSort { get; set; }
        public string SubjectSort { get; set; }

        #endregion
        public string CurrentSortOrder { get; set; }

        //Paging Props
        public IPagedList<Course> PagedCourses { get; set; }
        public IPagedList<Assignment> PagedAssignments { get; set; }
        public IPagedList<Student> PagedStudents { get; set; }
        public IPagedList<Trainer> PagedTrainers { get; set; }

        //View Props
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
        
       
    }
}