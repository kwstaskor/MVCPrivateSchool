using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;
using MVCSchool.UnitOfWork;


namespace MVCSchool.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public ActionResult Course(string searchByName, string sortOrder)
        {
            var courses = unitOfWork.Courses.Get();

            return View(courses);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DbDelete(int? id)
        {
            var course = unitOfWork.Courses.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            unitOfWork.Courses.Remove(course);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["StatusDel"] = $"You Have Successfully Deleted {course.Title} {course.Stream}";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vm = new CourseViewModel(unitOfWork);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course, int[] studentList, int[] trainerList, int[] assignmentList)
        {

            if (!ModelState.IsValid)
            {
                var vm = new CourseViewModel(unitOfWork);
                return RedirectToAction("Create", vm);
            }

            unitOfWork.Courses.AssignStudentsToCourse(course, studentList);
            unitOfWork.Courses.AssignTrainersToCourse(course, trainerList);
            unitOfWork.Courses.AssignAssignmentsToCourse(course, assignmentList);

            unitOfWork.Courses.Add(course);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Created {course.Title} {course.Stream}";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = unitOfWork.Courses.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var vm = new CourseViewModel(unitOfWork,course);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course, int[] studentEdit, int[] trainerEdit, int[] assignmentEdit)
        {
            if (!ModelState.IsValid)
            {
                var vm = new CourseViewModel(unitOfWork, course);
                return RedirectToAction("Edit", vm);
            }

            unitOfWork.Courses.AttachStudentsCourse(course);
            unitOfWork.Courses.ClearCourseStudents(course);
            unitOfWork.Courses.AssignStudentsToCourse(course, studentEdit);

            unitOfWork.Courses.AttachTrainersCourse(course);
            unitOfWork.Courses.ClearCourseTrainers(course);
            unitOfWork.Courses.AssignTrainersToCourse(course, trainerEdit);

            unitOfWork.Courses.AttachAssignmentsCourse(course);
            unitOfWork.Courses.ClearCourseAssignments(course);
            unitOfWork.Courses.AssignAssignmentsToCourse(course, assignmentEdit);

            unitOfWork.Courses.Edit(course);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Edited {course.Title} {course.Stream}";
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