using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class StudentsRepos : Repository<Student>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public StudentsRepos(ApplicationDbContext context) : base(context)
        {
        }

        public void AssignCoursesToStudent(Student student, int[] courseEdit)
        {
            if (courseEdit is null) return;

            foreach (var id in courseEdit)
            {
                var course = DbContext.CoursesDbSet.Find(id);
                if (!(course is null))
                {
                    student.Courses.Add(course);
                }
            }

            DbContext.SaveChanges();
        }
        
        public void AssignAssignmentsToStudent(Student student, int[] assignmentIds)
        {
            if (assignmentIds is null) return;

            foreach (var id in assignmentIds)
            {
                var assignment = DbContext.AssignmentsDbSet.Find(id);
                if (!(assignment is null))
                {
                    student.Assignments.Add(assignment);
                }
            }

            DbContext.SaveChanges();
        }

        public void ClearStudentCourses(Student student)
        {
            student.Courses.Clear();
            DbContext.SaveChanges();
        }
           public void ClearStudentAssignments(Student student)
        {
            student.Assignments.Clear();
            DbContext.SaveChanges();
        }

        public void AttachStudentCourses(Student student)
        {
            DbContext.StudentsDbSet.Attach(student);

            DbContext.Entry(student).Collection("Courses").Load();
        }
        
        public void AttachStudentAssignments(Student student)
        {
            DbContext.StudentsDbSet.Attach(student);

            DbContext.Entry(student).Collection("Assignments").Load();
        }

    }
}