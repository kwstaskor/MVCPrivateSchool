using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Controllers
{
    [Authorize]
    public class TrainerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public TrainerController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public ActionResult Trainer()
        {
            var trainers = unitOfWork.Trainers.Get();

            return View("Trainer", trainers);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = unitOfWork.Trainers.FindById(id);

            return View(trainer);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = unitOfWork.Trainers.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trainer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DbDelete(int? id)
        {
            var trainer = unitOfWork.Trainers.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            unitOfWork.Trainers.Remove(trainer);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["StatusDel"] = $"You Have Successfully Deleted {trainer.FirstName} {trainer.LastName}";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vm = new TrainerViewModel(unitOfWork);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "TrainerId , FirstName , LastName , YearsOfExperience , PhotoUrl , Subject , Bio")] Trainer trainer, int[] courseList)
        {
            if (!ModelState.IsValid)
            {
                var vm = new TrainerViewModel(unitOfWork);
                return RedirectToAction("Create", vm);
            }

            unitOfWork.Trainers.AssignCoursesToTrainer(trainer, courseList);
            unitOfWork.Trainers.Add(trainer);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Created {trainer.FirstName} {trainer.LastName}";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = unitOfWork.Trainers.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var vm = new TrainerViewModel(unitOfWork , trainer);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "TrainerId , FirstName , LastName , YearsOfExperience , PhotoUrl , Subject , Bio")] Trainer trainer, int[] courseEdit)
        {
            if (!ModelState.IsValid)
            {
               var vm = new TrainerViewModel(unitOfWork , trainer);
                return RedirectToAction("Edit", vm);
            }

            unitOfWork.Trainers.AttachTrainerCourses(trainer);
            unitOfWork.Trainers.ClearTrainerCourses(trainer);
            unitOfWork.Trainers.AssignCoursesToTrainer(trainer, courseEdit);
            unitOfWork.Trainers.Edit(trainer);
            unitOfWork.Save();

            TempData["ShowAlert"] = true;
            TempData["Status"] = $"You Have Successfully Edited {trainer.FirstName} {trainer.LastName}";
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