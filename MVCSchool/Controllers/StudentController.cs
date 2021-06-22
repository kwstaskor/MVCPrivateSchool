using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Controllers
{
    [Authorize]
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

            return View("Student", students);
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

            TempData["ShowAlert"] = true;
            TempData["StatusDel"] = $"You Have Successfully Deleted {student.FirstName} {student.LastName}";
            return RedirectToAction("Index" , "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vm = new StudentViewModel(unitOfWork);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "StudentId , FirstName , LastName , DateOfBirth , TuitionFee")] Student student,int[] courseList , int[] assignmentList)
        {
            if (!ModelState.IsValid)
            {
                var vm = new StudentViewModel(unitOfWork);
                return RedirectToAction("Create", vm);
            }

            unitOfWork.Students.AssignCoursesToStudent(student,courseList);
            unitOfWork.Students.AssignAssignmentsToStudent(student,assignmentList);

            unitOfWork.Students.Add(student);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Created {student.FirstName} {student.LastName}";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = unitOfWork.Students.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var vm = new StudentViewModel(unitOfWork,student);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "StudentId , FirstName , LastName , DateOfBirth , TuitionFee")] Student student , int[] courseEdit , int[] assignmentEdit)
        {
            if (!ModelState.IsValid)
            {
                var vm = new StudentViewModel(unitOfWork, student);
                return RedirectToAction("Edit", vm);
            }

            unitOfWork.Students.AttachStudentCourses(student);
            unitOfWork.Students.ClearStudentCourses(student);
            unitOfWork.Students.AssignCoursesToStudent(student,courseEdit);

            unitOfWork.Students.AttachStudentAssignments(student);
            unitOfWork.Students.ClearStudentAssignments(student);
            unitOfWork.Students.AssignAssignmentsToStudent(student,assignmentEdit);

            unitOfWork.Students.Edit(student);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Edited {student.FirstName} {student.LastName}";
            return RedirectToAction("Index", "Admin");
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