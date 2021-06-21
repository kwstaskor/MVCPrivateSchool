using System.Collections.Generic;
using System.Linq;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;

namespace MVCSchool.Helper
{
    public static class Sorting
    {
        public static void SortingViewModel(string sortOrder, AdminViewModel viewModel)
        {
            viewModel.CurrentSortOrder = sortOrder;

            viewModel.Courses = SortingCourses(sortOrder, viewModel);
            viewModel.Assignments = SortingAssignments(sortOrder, viewModel);
            viewModel.Students = SortingStudents(sortOrder, viewModel);
            viewModel.Trainers = SortingTrainers(sortOrder, viewModel);
        }

        private static IEnumerable<Course> SortingCourses(string sortOrder, AdminViewModel viewModel)
        {
            viewModel.TitleCSort = string.IsNullOrEmpty(sortOrder) ? "TitleCDesc" : "";
            viewModel.StreamSort = sortOrder == "StreamAsc" ? "StreamDesc" : "StreamAsc";
            viewModel.TypeSort = sortOrder == "TypeAsc" ? "TypeDesc" : "TypeAsc";
            viewModel.SdSort = sortOrder == "SdAsc" ? "SdDesc" : "SdAsc";
            viewModel.EdSort = sortOrder == "EdAsc" ? "EdDesc" : "EdAsc";

            switch (sortOrder)
            {
                case "TitleCDesc": viewModel.Courses = viewModel.Courses.OrderByDescending(c => c.Title).ToList(); break;

                case "StreamAsc": viewModel.Courses = viewModel.Courses.OrderBy(c => c.Stream).ToList(); break;
                case "StreamDesc": viewModel.Courses = viewModel.Courses.OrderByDescending(c => c.Stream).ToList(); break;

                case "TypeAsc": viewModel.Courses = viewModel.Courses.OrderBy(c => c.Type).ToList(); break;
                case "TypeDesc": viewModel.Courses = viewModel.Courses.OrderByDescending(c => c.Type).ToList(); break;

                case "SdAsc": viewModel.Courses = viewModel.Courses.OrderBy(c => c.StartDate).ToList(); break;
                case "SdDesc": viewModel.Courses = viewModel.Courses.OrderByDescending(c => c.StartDate).ToList(); break;

                case "EdAsc": viewModel.Courses = viewModel.Courses.OrderBy(c => c.EndDate).ToList(); break;
                case "EdDesc": viewModel.Courses = viewModel.Courses.OrderByDescending(c => c.EndDate).ToList(); break;

                default: viewModel.Courses = viewModel.Courses.OrderBy(t => t.Title).ToList(); break;
            }

            return viewModel.Courses;
        }

        private static IEnumerable<Assignment> SortingAssignments(string sortOrder, AdminViewModel viewModel)
        {
            viewModel.TitleASort = string.IsNullOrEmpty(sortOrder) ? "TitleADesc" : "";
            viewModel.DescriptionSort = sortOrder == "DescriptionAsc" ? "DescriptionDesc" : "DescriptionAsc";
            viewModel.SubmissionSort = sortOrder == "SubAsc" ? "SubDesc" : "SubAsc";
            viewModel.OralSort = sortOrder == "OralAsc" ? "OralDesc" : "OralAsc";
            viewModel.TotalSort = sortOrder == "TotalAsc" ? "TotalDesc" : "TotalAsc";

            switch (sortOrder)
            {
                case "TitleADesc": viewModel.Assignments = viewModel.Assignments.OrderByDescending(c => c.Title).ToList(); break;

                case "DescriptionAsc": viewModel.Assignments = viewModel.Assignments.OrderBy(a => a.Description).ToList(); break;
                case "DescriptionDesc": viewModel.Assignments = viewModel.Assignments.OrderByDescending(a => a.Description).ToList(); break;

                case "SubAsc": viewModel.Assignments = viewModel.Assignments.OrderBy(a => a.Submission).ToList(); break;
                case "SubDesc": viewModel.Assignments = viewModel.Assignments.OrderByDescending(a => a.Submission).ToList(); break;

                case "OralAsc": viewModel.Assignments = viewModel.Assignments.OrderBy(a => a.OralMark).ToList(); break;
                case "OralDesc": viewModel.Assignments = viewModel.Assignments.OrderByDescending(a => a.OralMark).ToList(); break;

                case "TotalAsc": viewModel.Assignments = viewModel.Assignments.OrderBy(a => a.TotalMark).ToList(); break;
                case "TotalDesc": viewModel.Assignments = viewModel.Assignments.OrderByDescending(a => a.TotalMark).ToList(); break;

                default: viewModel.Assignments = viewModel.Assignments.OrderBy(t => t.Title).ToList(); break;
            }

            return viewModel.Assignments;
        }

        private static IEnumerable<Student> SortingStudents(string sortOrder, AdminViewModel viewModel)
        {
            viewModel.FirstNameSSort = string.IsNullOrEmpty(sortOrder) ? "FirstNameSDesc" : "";
            viewModel.LastNameSSort = sortOrder == "LastNameSAsc" ? "LastNameSDesc" : "LastNameSAsc";
            viewModel.DobSSort = sortOrder == "DobSAsc" ? "DobSDesc" : "DobSAsc";
            viewModel.FeeSSort = sortOrder == "FeeSAsc" ? "FeeSDesc" : "FeeSAsc";

            switch (sortOrder)
            {
                case "FirstNameSDesc": viewModel.Students = viewModel.Students.OrderByDescending(s => s.FirstName).ToList(); break;

                case "LastNameSAsc": viewModel.Students = viewModel.Students.OrderBy(s => s.LastName).ToList(); break;
                case "LastNameSDesc": viewModel.Students = viewModel.Students.OrderByDescending(s => s.LastName).ToList(); break;

                case "DobSAsc": viewModel.Students = viewModel.Students.OrderBy(s => s.DateOfBirth).ToList(); break;
                case "DobSDesc": viewModel.Students = viewModel.Students.OrderByDescending(s => s.DateOfBirth).ToList(); break;

                case "FeeSAsc": viewModel.Students = viewModel.Students.OrderBy(s => s.TuitionFee).ToList(); break;
                case "FeeSDesc": viewModel.Students = viewModel.Students.OrderByDescending(s => s.TuitionFee).ToList(); break;

                default: viewModel.Students = viewModel.Students.OrderBy(s => s.FirstName).ToList(); break;
            }

            return viewModel.Students;
        }

        private static IEnumerable<Trainer> SortingTrainers(string sortOrder, AdminViewModel viewModel)
        {
            viewModel.FirstNameTSort = string.IsNullOrEmpty(sortOrder) ? "FirstNameTDesc" : "";
            viewModel.LastNameTSort = sortOrder == "LastNameTAsc" ? "LastNameTDesc" : "LastNameTAsc";
            viewModel.SubjectSort = sortOrder == "SubjectAsc" ? "DobSDesc" : "SubjectAsc";


            switch (sortOrder)
            {
                case "FirstNameTDesc": viewModel.Trainers = viewModel.Trainers.OrderByDescending(t => t.FirstName).ToList(); break;

                case "LastNameTAsc": viewModel.Trainers = viewModel.Trainers.OrderBy(t => t.LastName).ToList(); break;
                case "LastNameTDesc": viewModel.Trainers = viewModel.Trainers.OrderByDescending(s => s.LastName).ToList(); break;

                case "SubjectAsc": viewModel.Trainers = viewModel.Trainers.OrderBy(t => t.Subject).ToList(); break;
                case "SubjectDesc": viewModel.Trainers = viewModel.Trainers.OrderByDescending(t => t.Subject).ToList(); break;


                default: viewModel.Trainers = viewModel.Trainers.OrderBy(s => s.FirstName).ToList(); break;
            }

            return viewModel.Trainers;
        }
    }
}