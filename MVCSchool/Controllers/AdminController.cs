using System.Net;
using System.Web.Mvc;
using MVCSchool.Helper;
using MVCSchool.Models.ViewModels;
using MVCSchool.UnitOfWork;


namespace MVCSchool.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public ActionResult Index(string searchByNameA, string searchByNameC, string searchByNameS, string searchByNameT,
            string sortOrder, int? pageC, int? pageA, int? pageS, int? pageT)
        {
            var viewModel = new AdminViewModel(unitOfWork);

            Filtering.FilteringViewModel(searchByNameA, searchByNameC, searchByNameS, searchByNameT, viewModel);

            Sorting.SortingViewModel(sortOrder, viewModel);

            Paging.PagingViewModel(pageC, pageA, pageS, pageT, viewModel);

            return User.IsInRole("Admin") ? (ActionResult) View(viewModel) : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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