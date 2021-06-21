using System.Collections.Generic;
using System.Linq;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;

namespace MVCSchool.Helper
{
    public static class Filtering
    {
        public static void FilteringViewModel(string searchByNameA, string searchByNameC, string searchByNameS,
           string searchByNameT, AdminViewModel viewModel)
        {
            viewModel.SearchByNameAss = searchByNameA;
            viewModel.SearchByNameCrs = searchByNameC;
            viewModel.SearchByNameStd = searchByNameS;
            viewModel.SearchByNameTr = searchByNameT;

            viewModel.Courses = Filtering.FilteringCourses(searchByNameC, viewModel.Courses);
            viewModel.Assignments = Filtering.FilteringAssignments(searchByNameA, viewModel.Assignments);
            viewModel.Students = Filtering.FilteringStudents(searchByNameS, viewModel.Students);
            viewModel.Trainers = Filtering.FilteringTrainers(searchByNameT, viewModel.Trainers);
        }

        private static IEnumerable<Course> FilteringCourses(string searchByNameC, IEnumerable<Course> courses)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameC))
            {
                courses = courses.Where(c => c.Title.ToUpper().Contains(searchByNameC.ToUpper())).ToList();
            }

            return courses;
        }

        private static IEnumerable<Student> FilteringStudents(string searchByNameS, IEnumerable<Student> students)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameS))
            {
                students = students.Where(s => s.FirstName.ToUpper().Contains(searchByNameS.ToUpper())).ToList();
            }

            return students;
        }

        private static IEnumerable<Assignment> FilteringAssignments(string searchByNameA, IEnumerable<Assignment> assignments)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameA))
            {
                assignments = assignments.Where(c => c.Title.ToUpper().Contains(searchByNameA.ToUpper())).ToList();
            }

            return assignments;
        }

        private static IEnumerable<Trainer> FilteringTrainers(string searchByNameT, IEnumerable<Trainer> trainers)
        {
            if (!string.IsNullOrWhiteSpace(searchByNameT))
            {
                trainers = trainers.Where(s => s.FirstName.ToUpper().Contains(searchByNameT.ToUpper())).ToList();
            }

            return trainers;
        }
    }
}
