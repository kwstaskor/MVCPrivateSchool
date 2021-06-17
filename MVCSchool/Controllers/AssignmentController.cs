using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AssignmentController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public ActionResult Assignment()
        {
            var assignments = unitOfWork.Assignments.Get();

            return User.IsInRole("Admin") ? View("Assignment", assignments) : View("AssignmentReadOnly", assignments);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = unitOfWork.Assignments.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(assignment);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = unitOfWork.Assignments.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(assignment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DbDelete(int? id)
        {
            var assignment = unitOfWork.Assignments.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            unitOfWork.Assignments.Remove(assignment);
            unitOfWork.Save();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddStudentsViewBag();
            AddCoursesViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "AssignmentId,Title,Description,Submission,OralMark,TotalMark")] Assignment assignment, int[] courseList , int[] studentList)
        {

            if (!ModelState.IsValid)
            {
                AddStudentsViewBag();
                AddCoursesViewBag();
                return RedirectToAction("Create", assignment);
            }

            unitOfWork.Assignments.AssignCoursesToAssignment(assignment, courseList);
            unitOfWork.Assignments.AssignStudentsToAssignment(assignment, studentList);

            unitOfWork.Assignments.Add(assignment);
            unitOfWork.Save();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = unitOfWork.Assignments.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            EditCoursesViewBag(assignment);
            EditStudentsViewBag(assignment);
            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "AssignmentId,Title,Description,Submission,OralMark,TotalMark")] Assignment assignment, int[] courseEdit , int[] studentEdit)
        {
            if (!ModelState.IsValid)
            {
                EditCoursesViewBag(assignment);
                EditStudentsViewBag(assignment);
                return RedirectToAction("Edit", assignment);
            }

            unitOfWork.Assignments.AttachAssignmentCourses(assignment);
            unitOfWork.Assignments.ClearAssignmentCourses(assignment);
            unitOfWork.Assignments.AssignCoursesToAssignment(assignment, courseEdit);

            unitOfWork.Assignments.AttachAssignmentStudents(assignment);
            unitOfWork.Assignments.ClearAssignmentStudents(assignment);
            unitOfWork.Assignments.AssignStudentsToAssignment(assignment, studentEdit);

            unitOfWork.Assignments.Edit(assignment);
            unitOfWork.Save();

            return RedirectToAction("Index", "Admin");
        }

        private void EditCoursesViewBag(Assignment assignment)
        {
            var courses = unitOfWork.Courses.Get();
            unitOfWork.Assignments.AttachAssignmentCourses(assignment);
            var assignmentCoursesIds = assignment.Courses.Select(course => course.CourseId);

            ViewBag.courseEdit = courses.ToList().Select(c => new SelectListItem()
            {
                Value = c.CourseId.ToString(),
                Text = string.Format($"{c.Title} {c.Stream}"),
                Selected = assignmentCoursesIds.Any(selected => selected == c.CourseId)
            });
        }

        private void EditStudentsViewBag(Assignment assignment)
        {
            var students = unitOfWork.Students.Get();
            unitOfWork.Assignments.AttachAssignmentStudents(assignment);
            var assignmentStudentsIds = assignment.Students.Select(student => student.StudentId);

            ViewBag.studentEdit = students.ToList().Select(s => new SelectListItem()
            {
                Value = s.StudentId.ToString(),
                Text = string.Format($"{s.FirstName} {s.LastName}"),
                Selected = assignmentStudentsIds.Any(selected => selected == s.StudentId)
            });
        }

        private void AddCoursesViewBag()
        {
            var courses = unitOfWork.Courses.Get();
            var selectList = new MultiSelectList(courses, "CourseId", "Title");
            ViewBag.courseList = selectList;
        }
        private void AddStudentsViewBag()
        {
            var students = unitOfWork.Students.Get().ToList().Select(s=> new
            {
                StudentId = s.StudentId,
                Name = string.Format($"{s.FirstName} {s.LastName}")
            });
            var selectList = new MultiSelectList(students, "StudentId", "Name");
            ViewBag.studentList = selectList;
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