using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;
using PagedList;

namespace MVCSchool.Helper
{
    public static class AdminUtilities
    {
        #region Paging
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
        #endregion

        #region Filtering
        public static IEnumerable<Course> FilteringCourses(string searchByNameC, IEnumerable<Course> courses)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameC))
            {
                courses = courses.Where(c => c.Title.ToUpper().Contains(searchByNameC.ToUpper())).ToList();
            }

            return courses;
        }

        public static IEnumerable<Student> FilteringStudents(string searchByNameS, IEnumerable<Student> students)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameS))
            {
                students = students.Where(s => s.FirstName.ToUpper().Contains(searchByNameS.ToUpper())).ToList();
            }

            return students;
        }

        public static IEnumerable<Assignment> FilteringAssignments(string searchByNameA, IEnumerable<Assignment> assignments)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameA))
            {
                assignments = assignments.Where(c => c.Title.ToUpper().Contains(searchByNameA.ToUpper())).ToList();
            }

            return assignments;
        }

        public static IEnumerable<Trainer> FilteringTrainers(string searchByNameT, IEnumerable<Trainer> trainers)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameT))
            {
                trainers = trainers.Where(s => s.FirstName.ToUpper().Contains(searchByNameT.ToUpper())).ToList();
            }

            return trainers;
        }
    }
} 
#endregion