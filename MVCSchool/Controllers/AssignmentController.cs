using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Controllers
{
    [Authorize]
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

            return View("Assignment", assignments);
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

            TempData["ShowAlert"] = true;
            TempData["StatusDel"] = $"You Have Successfully Deleted {assignment.Title} {assignment.Description}";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vm = new AssignmentViewModel(unitOfWork);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "AssignmentId,Title,Description,Submission,OralMark,TotalMark")] Assignment assignment, int[] courseList , int[] studentList)
        {

            if (!ModelState.IsValid)
            {
                var vm = new AssignmentViewModel(unitOfWork);
                return RedirectToAction("Create", vm);
            }

            unitOfWork.Assignments.AssignCoursesToAssignment(assignment, courseList);
            unitOfWork.Assignments.AssignStudentsToAssignment(assignment, studentList);

            unitOfWork.Assignments.Add(assignment);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Created {assignment.Title} {assignment.Description}";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = unitOfWork.Assignments.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var vm = new AssignmentViewModel(unitOfWork,assignment);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "AssignmentId,Title,Description,Submission,OralMark,TotalMark")] Assignment assignment, int[] courseEdit , int[] studentEdit)
        {
            if (!ModelState.IsValid)
            {
                var vm = new AssignmentViewModel(unitOfWork,assignment);
                return RedirectToAction("Edit", vm);
            }

            unitOfWork.Assignments.AttachAssignmentCourses(assignment);
            unitOfWork.Assignments.ClearAssignmentCourses(assignment);
            unitOfWork.Assignments.AssignCoursesToAssignment(assignment, courseEdit);

            unitOfWork.Assignments.AttachAssignmentStudents(assignment);
            unitOfWork.Assignments.ClearAssignmentStudents(assignment);
            unitOfWork.Assignments.AssignStudentsToAssignment(assignment, studentEdit);

            unitOfWork.Assignments.Edit(assignment);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Edited {assignment.Title} {assignment.Description}";
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