using MVCSchool.Models.ViewModels;
using PagedList;

namespace MVCSchool.Helper
{
    public static class Paging
    {
        public static void PagingViewModel(int? pageC, int? pageA, int? pageS, int? pageT, AdminViewModel viewModel)
        {
            PagingCourses(pageC, viewModel);
            PagingAssignments(pageA, viewModel);
            PagingStudents(pageS, viewModel);
            PagingTrainers(pageT, viewModel);
        }

        private static void PagingCourses(int? pageC, AdminViewModel viewModel)
        {
            var pageNumber = pageC ?? 1;
            var pageSize = 4;
            viewModel.PagedCourses = viewModel.Courses.ToPagedList(pageNumber, pageSize);
        }

        private static void PagingAssignments(int? pageA, AdminViewModel viewModel)
        {
            var pageNumber = pageA ?? 1;
            var pageSize = 4;
            viewModel.PagedAssignments = viewModel.Assignments.ToPagedList(pageNumber, pageSize);
        }

        private static void PagingStudents(int? pageS, AdminViewModel viewModel)
        {
            var pageNumber = pageS ?? 1;
            var pageSize = 4;
            viewModel.PagedStudents = viewModel.Students.ToPagedList(pageNumber, pageSize);
        }

        private static void PagingTrainers(int? pageT, AdminViewModel viewModel)
        {
            var pageNumber = pageT ?? 1;
            var pageSize = 3;
            viewModel.PagedTrainers = viewModel.Trainers.ToPagedList(pageNumber, pageSize);
        }
    }
}
