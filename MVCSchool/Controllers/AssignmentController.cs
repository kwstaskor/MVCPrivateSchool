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

            return User.IsInRole("Admin") ? View("Assignment", assignments) : View("AssignmentReadOnly" , assignments);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = unitOfWork.Assignments.FindById(id);

            if(assignment== null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

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

        [HttpPost , ActionName("Delete")]
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
            var courses = unitOfWork.Courses.Get();
            var selectList = new MultiSelectList(courses, "CourseId", "Title");
            ViewBag.courseList = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "AssignmentId,Title,Description,Submission,OralMark,TotalMark")] Assignment assignment, int[] courseList)
        {
            if (!(courseList is null))
            {
                foreach (var id in courseList)
                {
                    var course = unitOfWork.Courses.FindById(id);
                    assignment.Courses.Add(course);
                }
            }

            if (!ModelState.IsValid) return RedirectToAction("Create", assignment);

            unitOfWork.Assignments.Add(assignment);
            unitOfWork.Save();
            return RedirectToAction("Index", "Admin");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = unitOfWork.Assignments.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "AssignmentId,Title,Description,Submission,OralMark,TotalMark")] Assignment assignment , int[] courses)
        {
            if (courses != null)
            {
                assignment.Courses.Clear();
                foreach (var id in courses)
                {
                    var course = unitOfWork.Courses.FindById(id);
                    assignment.Courses.Add(course);
                }
            }
           

            if (!ModelState.IsValid) return RedirectToAction("Edit", assignment);

            unitOfWork.Assignments.Edit(assignment);
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