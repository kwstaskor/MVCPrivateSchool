using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public ActionResult Index(string searchByNameA, string searchByNameC, string searchByNameS, string searchByNameT,
            string sortOrder, int? pSize, int? pNumber)
        {
            var viewModel = new AdminViewModel
            {
                Courses = unitOfWork.Courses.Get(),
                Students = unitOfWork.Students.Get(),
                Trainers = unitOfWork.Trainers.Get(),
                Assignments = unitOfWork.Assignments.Get()
            };

            FilteringViewModel(searchByNameA, searchByNameC, searchByNameS,searchByNameT, viewModel);

            SortingViewModel(sortOrder, viewModel);

            if (User.IsInRole("Admin"))
            {
                return View(viewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private void SortingViewModel(string sortOrder, AdminViewModel viewModel)
        {
            ViewBag.CurrentSortOrder = sortOrder;

            viewModel.Courses = SortingCourses(sortOrder, viewModel.Courses);
            viewModel.Assignments = SortingAssignments(sortOrder, viewModel.Assignments);
            viewModel.Students = SortingStudents(sortOrder, viewModel.Students);
            viewModel.Trainers = SortingTrainers(sortOrder, viewModel.Trainers);
        }
        private IEnumerable<Course> SortingCourses(string sortOrder, IEnumerable<Course> courses)
        {
            ViewBag.TitleCSort = string.IsNullOrEmpty(sortOrder) ? "TitleCDesc" : "";
            ViewBag.StreamSort = sortOrder == "StreamAsc" ? "StreamDesc" : "StreamAsc";
            ViewBag.TypeSort = sortOrder == "TypeAsc" ? "TypeDesc" : "TypeAsc";
            ViewBag.SdSort = sortOrder == "SdAsc" ? "SdDesc" : "SdAsc";
            ViewBag.EdSort = sortOrder == "EdAsc" ? "EdDesc" : "EdAsc";

            switch (sortOrder)
            {
                case "TitleCDesc": courses = courses.OrderByDescending(c => c.Title).ToList(); break;

                case "StreamAsc": courses = courses.OrderBy(c => c.Stream).ToList(); break;
                case "StreamDesc": courses = courses.OrderByDescending(c => c.Stream).ToList(); break;

                case "TypeAsc": courses = courses.OrderBy(c => c.Type).ToList(); break;
                case "TypeDesc": courses = courses.OrderByDescending(c => c.Type).ToList(); break;

                case "SdAsc": courses = courses.OrderBy(c => c.StartDate).ToList(); break;
                case "SdDesc": courses = courses.OrderByDescending(c => c.StartDate).ToList(); break;

                case "EdAsc": courses = courses.OrderBy(c => c.EndDate).ToList(); break;
                case "EdDesc": courses = courses.OrderByDescending(c => c.EndDate).ToList(); break;

                default: courses = courses.OrderBy(t => t.Title).ToList(); break;
            }

            return courses;
        }
        private IEnumerable<Assignment> SortingAssignments(string sortOrder, IEnumerable<Assignment> assignments)
        {
            ViewBag.TitleASort = string.IsNullOrEmpty(sortOrder) ? "TitleADesc" : "";
            ViewBag.DescriptionSort = sortOrder == "DescriptionAsc" ? "DescriptionDesc" : "DescriptionAsc";
            ViewBag.SubmissionSort = sortOrder == "SubAsc" ? "SubDesc" : "SubAsc";
            ViewBag.OralSort = sortOrder == "OralAsc" ? "OralDesc" : "OralAsc";
            ViewBag.TotalSort = sortOrder == "TotalAsc" ? "TotalDesc" : "TotalAsc";

            switch (sortOrder)
            {
                case "TitleADesc": assignments = assignments.OrderByDescending(c => c.Title).ToList(); break;

                case "DescriptionAsc": assignments = assignments.OrderBy(a => a.Description).ToList(); break;
                case "DescriptionDesc": assignments = assignments.OrderByDescending(a => a.Description).ToList(); break;

                case "SubAsc": assignments = assignments.OrderBy(a => a.Submission).ToList(); break;
                case "SubDesc": assignments = assignments.OrderByDescending(a => a.Submission).ToList(); break;

                case "OralAsc": assignments = assignments.OrderBy(a => a.OralMark).ToList(); break;
                case "OralDesc": assignments = assignments.OrderByDescending(a => a.OralMark).ToList(); break;

                case "TotalAsc": assignments = assignments.OrderBy(a => a.TotalMark).ToList(); break;
                case "TotalDesc": assignments = assignments.OrderByDescending(a => a.TotalMark).ToList(); break;

                default: assignments = assignments.OrderBy(t => t.Title).ToList(); break;
            }

            return assignments;
        }
        private IEnumerable<Student> SortingStudents(string sortOrder, IEnumerable<Student> students)
        {
            ViewBag.FirstNameSSort = string.IsNullOrEmpty(sortOrder) ? "FirstNameSDesc" : "";
            ViewBag.LastNameSSort = sortOrder == "LastNameSAsc" ? "LastNameSDesc" : "LastNameSAsc";
            ViewBag.DobSSort = sortOrder == "DobSAsc" ? "DobSDesc" : "DobSAsc";
            ViewBag.FeeSSort = sortOrder == "FeeSAsc" ? "FeeSDesc" : "FeeSAsc";

            switch (sortOrder)
            {
                case "FirstNameSDesc": students = students.OrderByDescending(s => s.FirstName).ToList(); break;

                case "LastNameSAsc": students = students.OrderBy(s => s.LastName).ToList(); break;
                case "LastNameSDesc": students = students.OrderByDescending(s => s.LastName).ToList(); break;

                case "DobSAsc": students = students.OrderBy(s => s.DateOfBirth).ToList(); break;
                case "DobSDesc": students = students.OrderByDescending(s => s.DateOfBirth).ToList(); break;

                case "FeeSAsc": students = students.OrderBy(s => s.TuitionFee).ToList(); break;
                case "FeeSDesc": students = students.OrderByDescending(s => s.TuitionFee).ToList(); break;

                default: students = students.OrderBy(s => s.FirstName).ToList(); break;
            }

            return students;
        }
        private IEnumerable<Trainer> SortingTrainers(string sortOrder, IEnumerable<Trainer> trainers)
        {
            ViewBag.FirstNameTSort = string.IsNullOrEmpty(sortOrder) ? "FirstNameTDesc" : "";
            ViewBag.LastNameTSort = sortOrder == "LastNameTAsc" ? "LastNameTDesc" : "LastNameTAsc";
            ViewBag.SubjectSort = sortOrder == "SubjectAsc" ? "DobSDesc" : "SubjectAsc";
          

            switch (sortOrder)
            {
                case "FirstNameTDesc": trainers = trainers.OrderByDescending(t => t.FirstName).ToList(); break;

                case "LastNameTAsc": trainers = trainers.OrderBy(t => t.LastName).ToList(); break;
                case "LastNameTDesc": trainers = trainers.OrderByDescending(s => s.LastName).ToList(); break;

                case "SubjectAsc": trainers = trainers.OrderBy(t => t.Subject).ToList(); break;
                case "SubjectDesc": trainers = trainers.OrderByDescending(t => t.Subject).ToList(); break;
               

                default: trainers = trainers.OrderBy(s => s.FirstName).ToList(); break;
            }

            return trainers;

        }

        private void FilteringViewModel(string searchByNameA, string searchByNameC, string searchByNameS,
           string searchByNameT, AdminViewModel viewModel)
        {
            ViewBag.CurrentNameA = searchByNameA;
            ViewBag.CurrentNameC = searchByNameC;
            ViewBag.CurrentNameS = searchByNameS;
            ViewBag.CurrentNameT = searchByNameT;

            viewModel.Courses = FilteringCourses(searchByNameC, viewModel.Courses);
            viewModel.Assignments = FilteringAssignments(searchByNameA, viewModel.Assignments);
            viewModel.Students = FilteringStudents(searchByNameS, viewModel.Students);
            viewModel.Trainers = FilteringTrainers(searchByNameT, viewModel.Trainers);
        }
        private IEnumerable<Course> FilteringCourses(string searchByNameC, IEnumerable<Course> courses)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameC))
            {
                courses = courses.Where(c => c.Title.ToUpper().Contains(searchByNameC.ToUpper())).ToList();
            }

            return courses;
        }
        private IEnumerable<Student> FilteringStudents(string searchByNameS, IEnumerable<Student> students)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameS))
            {
                students = students.Where(s => s.FirstName.ToUpper().Contains(searchByNameS.ToUpper())).ToList();
            }

            return students;
        }
        private IEnumerable<Assignment> FilteringAssignments(string searchByNameA, IEnumerable<Assignment> assignments)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameA))
            {
                assignments = assignments.Where(c => c.Title.ToUpper().Contains(searchByNameA.ToUpper())).ToList();
            }

            return assignments;
        }
        private IEnumerable<Trainer> FilteringTrainers(string searchByNameT, IEnumerable<Trainer> trainers)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameT))
            {
                trainers = trainers.Where(s => s.FirstName.ToUpper().Contains(searchByNameT.ToUpper())).ToList();
            }

            return trainers;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}