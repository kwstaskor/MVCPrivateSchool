using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.UnitOfWork;


namespace MVCSchool.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseController()
        {
           unitOfWork =  new UnitOfWork.UnitOfWork();
        }

        public ActionResult Course(string searchByName , string sortOrder)
        {
            var courses = unitOfWork.Courses.Get();

            return View(User.IsInRole("Admin") ? "Course" : "CourseReadOnly", courses);
        }
      
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = unitOfWork.Courses.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = unitOfWork.Courses.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DbDelete(int? id)
        {
            var course = unitOfWork.Courses.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            unitOfWork.Courses.Remove(course);
            unitOfWork.Save();

            return RedirectToAction("Index" , "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddStudentsViewBag();
            AddTrainersViewBag();
            AddAssignmentsViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course , int[] studentList , int[] trainerList, int[] assignmentList)
        {

            if (!ModelState.IsValid)
            {
                AddStudentsViewBag();
                AddTrainersViewBag();
                AddAssignmentsViewBag();
                return RedirectToAction("Create", course);
            }
           
            unitOfWork.Courses.AssignStudentsToCourse(course,studentList);
            unitOfWork.Courses.AssignTrainersToCourse(course,trainerList);
            unitOfWork.Courses.AssignAssignmentsToCourse(course,assignmentList);

            unitOfWork.Courses.Add(course);
            unitOfWork.Save();

            return RedirectToAction("Index" , "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = unitOfWork.Courses.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            EditStudentsViewBag(course);
            EditTrainersViewBag(course);
            EditAssignmentsViewBag(course);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course, int[] studentEdit, int[] trainerEdit, int[] assignmentEdit)
        {
            if (!ModelState.IsValid)
            {
                EditStudentsViewBag(course);
                EditTrainersViewBag(course);
                EditAssignmentsViewBag(course);
                return RedirectToAction("Edit", course);
            }

            unitOfWork.Courses.AttachStudentsCourse(course);
            unitOfWork.Courses.ClearCourseStudents(course);
            unitOfWork.Courses.AssignStudentsToCourse(course,studentEdit);

            unitOfWork.Courses.AttachTrainersCourse(course);
            unitOfWork.Courses.ClearCourseTrainers(course);
            unitOfWork.Courses.AssignTrainersToCourse(course,trainerEdit);

            unitOfWork.Courses.AttachAssignmentsCourse(course);
            unitOfWork.Courses.ClearCourseAssignments(course);
            unitOfWork.Courses.AssignAssignmentsToCourse(course,assignmentEdit);

            unitOfWork.Courses.Edit(course);
            unitOfWork.Save();

            return RedirectToAction("Index", "Admin");
        }

        private void AddStudentsViewBag()
        {
            var students = unitOfWork.Students.Get().ToList().Select(s => new
            {
                StudentId = s.StudentId,
                Name = string.Format($"{s.FirstName} {s.LastName}")
            });
            var selectList = new MultiSelectList(students, "StudentId", "Name");
            ViewBag.studentList = selectList;
        }

        private void EditStudentsViewBag(Course course)
        {
            var students = unitOfWork.Students.Get();
            unitOfWork.Courses.AttachStudentsCourse(course);
            var courseStudentsIds = course.Students.Select(student => student.StudentId);

            ViewBag.studentEdit = students.ToList().Select(s => new SelectListItem()
            {
                Value = s.StudentId.ToString(),
                Text = string.Format($"{s.FirstName} {s.LastName}"),
                Selected = courseStudentsIds.Any(selected => selected == s.StudentId)
            });
        }

        private void AddTrainersViewBag()
        {
            var trainers = unitOfWork.Trainers.Get().ToList().Select(trainer => new
            {
                TrainerId = trainer.TrainerId,
                Name = string.Format($"{trainer.FirstName} {trainer.LastName}")
            });
            var selectList = new MultiSelectList(trainers, "TrainerId", "Name");
            ViewBag.trainerList = selectList;
        }

        private void EditTrainersViewBag(Course course)
        {
            var trainers = unitOfWork.Trainers.Get();
            unitOfWork.Courses.AttachTrainersCourse(course);
            var courseTrainersIds = course.Trainers.Select(trainer => trainer.TrainerId);

            ViewBag.trainerEdit = trainers.ToList().Select(trainer => new SelectListItem()
            {
                Value = trainer.TrainerId.ToString(),
                Text = string.Format($"{trainer.FirstName} {trainer.LastName}"),
                Selected = courseTrainersIds.Any(selected => selected == trainer.TrainerId)
            });
        }

        private void AddAssignmentsViewBag()
        {
            var assignments = unitOfWork.Assignments.Get();
            var selectList = new MultiSelectList(assignments, "AssignmentId", "Title");
            ViewBag.assignmentList = selectList;
        }

        private void EditAssignmentsViewBag(Course course)
        {
            var assignments = unitOfWork.Assignments.Get();
            unitOfWork.Courses.AttachAssignmentsCourse(course);
            var courseAssignmentsIds = course.Assignments.Select(assignment => assignment.AssignmentId);

            ViewBag.assignmentEdit = assignments.ToList().Select(a => new SelectListItem()
            {
                Value = a.AssignmentId.ToString(),
                Text = string.Format($"{a.Title} - {a.Description}"),
                Selected = courseAssignmentsIds.Any(selected => selected == a.AssignmentId)
            });
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