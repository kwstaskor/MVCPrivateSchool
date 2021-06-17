using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {

            #region Seed Students
            var s1 = new Student()
            {
                FirstName = "Kostas",
                LastName = "Papakostas",
                DateOfBirth = new DateTime(1991, 2, 28),
                TuitionFee = 2500
            };
            var s2 = new Student()
            {
                FirstName = "Xristos",
                LastName = "Xristou",
                DateOfBirth = new DateTime(1996, 10, 22),
                TuitionFee = 2500
            };
            var s3 = new Student()
            {
                FirstName = "Nikos",
                LastName = "Nikolaou",
                DateOfBirth = new DateTime(1989, 5, 8),
                TuitionFee = 2500
            };
            var s4 = new Student()
            {
                FirstName = "Aggeliki",
                LastName = "Aggelou",
                DateOfBirth = new DateTime(1995, 10, 28),
                TuitionFee = 2500
            };
            var s5 = new Student()
            {
                FirstName = "Petros",
                LastName = "Petrou",
                DateOfBirth = new DateTime(1990, 8, 28),
                TuitionFee = 2500
            };
            var s6 = new Student()
            {
                FirstName = "Giannis",
                LastName = "Ioannou",
                DateOfBirth = new DateTime(1991, 1, 2),
                TuitionFee = 2500
            };
            var s7 = new Student()
            {
                FirstName = "Xristofors",
                LastName = "Kotentos",
                DateOfBirth = new DateTime(1995, 1, 2),
                TuitionFee = 2500
            };
            var s8 = new Student()
            {
                FirstName = "Ioannis",
                LastName = "Mamouzakis",
                DateOfBirth = new DateTime(1991, 10, 20),
                TuitionFee = 2500
            };
            var s9 = new Student()
            {
                FirstName = "Anaksimandros",
                LastName = "Ioannou",
                DateOfBirth = new DateTime(1992, 3, 2),
                TuitionFee = 2500
            };
            var s10 = new Student()
            {
                FirstName = "Xristoforos",
                LastName = "Ioannou",
                DateOfBirth = new DateTime(1994, 2, 2),
                TuitionFee = 2500
            };

            var students = new List<Student>() { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 };

            foreach (var student in students)
            {
                context.StudentsDbSet.AddOrUpdate(s => new { s.FirstName, s.LastName }, student);
            } 
            #endregion

            #region Seed Assignments
            var a1 = new Assignment()
            {
                AssignmentId = 1,
                Title = "Private School",
                Description = "Individual Project",
                Submission = new DateTime(2021, 3, 31, 15, 00, 00),
                OralMark = 10,
                TotalMark = 100,
                Students = new Collection<Student>() { s1, s2 }
            };
            var a2 = new Assignment()
            {
                AssignmentId = 2,
                Title = "Data Bases",
                Description = "Individual Project",
                Submission = new DateTime(2021, 3, 31, 15, 00, 00),
                OralMark = 10,
                TotalMark = 100,
                Students = new Collection<Student>() { s1, s2 }

            };
            var a3 = new Assignment()
            {
                AssignmentId = 3,
                Title = "Web Security",
                Description = "Group Project",
                Submission = new DateTime(2021, 3, 15, 15, 00, 00),
                OralMark = 10,
                TotalMark = 100,
                Students = new Collection<Student>() { s3, s4 }
            };
            var a4 = new Assignment()
            {
                AssignmentId = 4,
                Title = "Web Application",
                Description = "Group Project",
                Submission = new DateTime(2021, 3, 15, 15, 00, 00),
                OralMark = 10,
                TotalMark = 100,
                Students = new Collection<Student>() { s3, s4 }
            };

            var assignments = new List<Assignment>() { a1, a2, a3, a4 };

            foreach (var assignment in assignments)
            {
                context.AssignmentsDbSet.AddOrUpdate(a => a.AssignmentId, assignment);
            } 
            #endregion

            #region Seed Trainers
            var t1 = new Trainer()
            {
                FirstName = "Gandalf",
                LastName = "TheGray",
                Subject = "Web Development"
            };
            var t2 = new Trainer()
            {
                FirstName = "Darth",
                LastName = "Vader",
                Subject = "Data Science"
            };
            var t3 = new Trainer()
            {
                FirstName = "Albus",
                LastName = "Dumbledore",
                Subject = "Mobile Development"
            };
            var t4 = new Trainer()
            {
                FirstName = "Severus",
                LastName = "Snape",
                Subject = "Desktop Development"
            };
            var t5 = new Trainer()
            {
                FirstName = "John",
                LastName = "Carter",
                Subject = "Gaming Development"
            };
            var t6 = new Trainer()
            {
                FirstName = "Peter",
                LastName = "Parker",
                Subject = "Web Development"
            };
            var t7 = new Trainer()
            {
                FirstName = "Bruce",
                LastName = "Wayne",
                Subject = "Mobile Development"
            };
            var t8 = new Trainer()
            {
                FirstName = "Bob",
                LastName = "The Fixer",
                Subject = "Web Security"
            };

            var trainers = new List<Trainer>() { t1, t2, t3, t4, t5, t6, t7, t8 };

            foreach (var trainer in trainers)
            {
                context.TrainersDbSet.AddOrUpdate(t => new { t.FirstName, t.LastName }, trainer);
            }

            #endregion

            #region Seed Courses
            var c1 = new Course
            {
                CourseId = 1,
                Title = "Spring Boot",
                Stream = "Java",
                Type = "Full Time",
                StartDate = new DateTime(2021, 2, 15),
                EndDate = new DateTime(2021, 5, 15),
                Assignments = new Collection<Assignment>() { a1, a2 },
                Students = new Collection<Student>() { s1, s2 },
                Trainers = new Collection<Trainer>() { t1, t5 }
            };
            var c2 = new Course
            {
                CourseId = 2,
                Title = "Hibernate",
                Stream = "Java",
                Type = "Part Time",
                StartDate = new DateTime(2021, 2, 15),
                EndDate = new DateTime(2021, 9, 15),
                Assignments = new Collection<Assignment>() { a1, a2 },
                Students = new Collection<Student>() { s1, s2, s3 },
                Trainers = new Collection<Trainer>() { t2 }
            };
            var c3 = new Course
            {
                CourseId = 3,
                Title = "Asp Net MVC",
                Stream = "Csharp",
                Type = "Full Time",
                StartDate = new DateTime(2021, 2, 15),
                EndDate = new DateTime(2021, 5, 15),
                Assignments = new Collection<Assignment>() { a3, a4 },
                Students = new Collection<Student>() { s4, s5, s9 },
                Trainers = new Collection<Trainer>() { t3 }

            };
            var c4 = new Course
            {
                CourseId = 4,
                Title = "Entity Framework",
                Stream = "Csharp",
                Type = "Part Time",
                StartDate = new DateTime(2021, 2, 15),
                EndDate = new DateTime(2021, 9, 15),
                Assignments = new Collection<Assignment>() { a3, a4 },
                Students = new Collection<Student>() { s4, s5, s6 },
                Trainers = new Collection<Trainer>() { t3, t4, t1 }
            };
            var c5 = new Course
            {
                CourseId = 5,
                Title = "Web Security",
                Stream = "Csharp",
                Type = "Part Time",
                StartDate = new DateTime(2021, 2, 15),
                EndDate = new DateTime(2021, 9, 15),
                Assignments = new Collection<Assignment>() { a3, a4 },
                Students = new Collection<Student>() { s7, s8, s10 },
                Trainers = new Collection<Trainer>() { t8 }
            };
            var c6 = new Course
            {
                CourseId = 6,
                Title = "Built a Mobile App",
                Stream = "Csharp",
                Type = "Part Time",
                StartDate = new DateTime(2021, 5, 15),
                EndDate = new DateTime(2022, 2, 15),
                Assignments = new Collection<Assignment>() { a3, a4 },
                Students = new Collection<Student>() { s2, s3, s10 },
                Trainers = new Collection<Trainer>() { t7, t2 }
            };
            var c7 = new Course
            {
                CourseId = 7,
                Title = "Built a Game",
                Stream = "Csharp",
                Type = "Part Time",
                StartDate = new DateTime(2021, 5, 15),
                EndDate = new DateTime(2022, 2, 15),
                Assignments = new Collection<Assignment>() { a2, a1 },
                Students = new Collection<Student>() { s7, s5, s10 },
                Trainers = new Collection<Trainer>() { t5, t6 }
            };

            var courses = new List<Course>() { c1, c2, c3, c4, c5, c6, c7 };

            foreach (var course in courses)
            {
                context.CoursesDbSet.AddOrUpdate(c => c.CourseId, course);
            } 
            #endregion

            context.SaveChanges();
        }

    }
}
