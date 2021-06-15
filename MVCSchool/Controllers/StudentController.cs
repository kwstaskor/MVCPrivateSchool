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
            var courses = unitOfWork.Courses.Get();
            var selectList = new MultiSelectList(courses, "CourseId", "Title");
            ViewBag.courseList = selectList;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "StudentId , FirstName , LastName , DateOfBirth , TuitionFee")] Student student,int[] courseList)
        {
            if (!(courseList is null))
            {
                foreach (var id in courseList)
                {
                    var course = unitOfWork.Courses.FindById(id);
                    student.Courses.Add(course);
                }
            }

            if (!ModelState.IsValid) return RedirectToAction("Create", student);

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

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "StudentId , FirstName , LastName , DateOfBirth , TuitionFee")] Student student)
        {
            if (!ModelState.IsValid) return RedirectToAction("Edit", student);

            unitOfWork.Students.Edit(student);
            unitOfWork.Save();

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