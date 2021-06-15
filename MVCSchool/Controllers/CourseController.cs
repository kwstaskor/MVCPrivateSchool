using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.UnitOfWork;
using PagedList;

namespace MVCSchool.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseController()
        {
           unitOfWork =  new UnitOfWork.UnitOfWork();
        }

        public ActionResult Course(string searchByName , string sortOrder , int? pSize , int? pNumber)
        {
            var courses = unitOfWork.Courses.Get();

            ViewBag.CurrentName = searchByName;
            ViewBag.CurrentSortOrder = sortOrder;

            courses = Filtering(searchByName, courses);

            courses = Sorting(sortOrder, courses);

            var pageSize = pSize ?? 5;
            var pageNumber = pNumber?? 1;

            return View(User.IsInRole("Admin") ? "Course" : "CourseReadOnly", courses.ToPagedList(pageNumber , pageSize));
        }


        private static IEnumerable<Course> Filtering(string searchByName, IEnumerable<Course> courses)
        {
            if (!string.IsNullOrWhiteSpace(searchByName))
            {
                courses = courses.Where(c => c.Title.ToUpper().Contains(searchByName.ToUpper())).ToList();
            }

            return courses;
        }

        private IEnumerable<Course> Sorting(string sortOrder, IEnumerable<Course> courses)
        {
            ViewBag.TitleSort = string.IsNullOrEmpty(sortOrder) ? "TitleDesc" : "";
            ViewBag.StreamSort = sortOrder == "StreamAsc" ? "StreamDesc" : "StreamAsc";
            ViewBag.TypeSort = sortOrder == "TypeAsc" ? "TypeDesc" : "TypeAsc";
            ViewBag.SdSort = sortOrder == "SdAsc" ? "SdDesc" : "SdAsc";
            ViewBag.EdSort = sortOrder == "EdAsc" ? "EdDesc" : "EdAsc";

            switch (sortOrder)
            {
                case "TitleDesc": courses = courses.OrderByDescending(c => c.Title).ToList(); break;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course)
        {

            if (!ModelState.IsValid) return RedirectToAction("Create", course);
           
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

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course)
        {
            if (!ModelState.IsValid) return RedirectToAction("Edit", course);

            unitOfWork.Courses.Edit(course);
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