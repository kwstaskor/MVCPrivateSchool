using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public StudentController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public ActionResult Student()
        {
            var students = unitOfWork.Students.Get();

            return User.IsInRole("Admin") ? View("Student", students) : View("StudentReadOnly", students);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var student = unitOfWork.Students.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View(student);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var student = unitOfWork.Students.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DbDelete(int? id)
        {
            var student = unitOfWork.Students.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            unitOfWork.Students.Remove(student);
            unitOfWork.Save();

            return RedirectToAction("Index" , "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddCoursesViewBag();
            AddAssignmentsViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "StudentId , FirstName , LastName , DateOfBirth , TuitionFee")] Student student,int[] courseList , int[] assignmentList)
        {
            if (!ModelState.IsValid)
            {
                AddCoursesViewBag();
                AddAssignmentsViewBag();
                return RedirectToAction("Create", student);
            }

            unitOfWork.Students.AssignCoursesToStudent(student,courseList);
            unitOfWork.Students.AssignAssignmentsToStudent(student,assignmentList);

            unitOfWork.Students.Add(student);
            unitOfWork.Save();
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = unitOfWork.Students.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            EditCoursesViewBag(student);
            EditAssignmentsViewBag(student);

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "StudentId , FirstName , LastName , DateOfBirth , TuitionFee")] Student student , int[] courseEdit , int[] assignmentEdit)
        {
            if (!ModelState.IsValid)
            {
                EditCoursesViewBag(student);
                EditAssignmentsViewBag(student);
                return RedirectToAction("Edit", student);
            }

            unitOfWork.Students.AttachStudentCourses(student);
            unitOfWork.Students.ClearStudentCourses(student);
            unitOfWork.Students.AssignCoursesToStudent(student,courseEdit);

            unitOfWork.Students.AttachStudentAssignments(student);
            unitOfWork.Students.ClearStudentAssignments(student);
            unitOfWork.Students.AssignAssignmentsToStudent(student,assignmentEdit);

            unitOfWork.Students.Edit(student);
            unitOfWork.Save();

            return RedirectToAction("Index", "Admin");
        }

        private void EditCoursesViewBag(Student student)
        {
            var courses = unitOfWork.Courses.Get();
            unitOfWork.Students.AttachStudentCourses(student);
            var studentCoursesIds = student.Courses.Select(course => course.CourseId);

            ViewBag.courseEdit = courses.ToList().Select(c => new SelectListItem()
            {
                Value = c.CourseId.ToString(),
                Text = string.Format($"{c.Title} {c.Stream}"),
                Selected = studentCoursesIds.Any(selected => selected == c.CourseId)
            });
        }
        
        private void EditAssignmentsViewBag(Student student)
        {
            var assignments = unitOfWork.Assignments.Get();
            unitOfWork.Students.AttachStudentAssignments(student);
            var studentCoursesIds = student.Assignments.Select(assignment => assignment.AssignmentId);

            ViewBag.assignmentEdit = assignments.ToList().Select(a => new SelectListItem()
            {
                Value = a.AssignmentId.ToString(),
                Text = string.Format($"{a.Title} - {a.Description}"),
                Selected = studentCoursesIds.Any(selected => selected == a.AssignmentId)
            });
        }

        private void AddCoursesViewBag()
        {
            var courses = unitOfWork.Courses.Get();
            var selectList = new MultiSelectList(courses, "CourseId", "Title");
            ViewBag.courseList = selectList;
        }

        private void AddAssignmentsViewBag()
        {
            var assignments = unitOfWork.Assignments.Get();
            var selectList = new MultiSelectList(assignments, "AssignmentId", "Title");
            ViewBag.assignmentList = selectList;
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