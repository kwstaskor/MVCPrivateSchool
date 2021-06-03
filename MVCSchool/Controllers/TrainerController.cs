using System.Net;
using System.Web.Mvc;
using MVCSchool.Models;
using MVCSchool.UnitOfWork;

namespace MVCSchool.Controllers
{
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
            
            return User.IsInRole("Admin") ? View("Trainer" , trainers) : View("TrainerReadOnly" , trainers);
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

            return RedirectToAction("Trainer");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbCreate([Bind(Include = "TrainerId , FirstName , LastName , Subject")] Trainer trainer)
        {
            if (!ModelState.IsValid) return RedirectToAction("Create", trainer);

            unitOfWork.Trainers.Add(trainer);
            unitOfWork.Save();
            return RedirectToAction("Trainer");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = unitOfWork.Trainers.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DbEdit([Bind(Include = "TrainerId , FirstName , LastName ,Subject")] Trainer trainer)
        {
            if (!ModelState.IsValid) return RedirectToAction("Edit", trainer);

            unitOfWork.Trainers.Edit(trainer);
            unitOfWork.Save();

            return RedirectToAction("Trainer");
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