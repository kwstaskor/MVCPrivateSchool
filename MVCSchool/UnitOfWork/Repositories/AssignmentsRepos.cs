using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class AssignmentsRepos : Repository<Assignment>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public AssignmentsRepos(ApplicationDbContext context) : base(context)
        {
        }

        public void AssignCoursesToAssignment(Assignment assignment, int[] courseEdit)
        {
            if (courseEdit is null) return;

            foreach (var id in courseEdit)
            {
                var course = DbContext.CoursesDbSet.Find(id);
                if (!(course is null))
                {
                    assignment.Courses.Add(course);
                }
            }

            DbContext.SaveChanges();
        }  

        public void AssignStudentsToAssignment(Assignment assignment, int[] studentIds)
        {
            if (studentIds is null) return;

            foreach (var id in studentIds)
            {
                var student = DbContext.StudentsDbSet.Find(id);
                if (!(student is null))
                {
                    assignment.Students.Add(student);
                }
            }

            DbContext.SaveChanges();
        }

        public void ClearAssignmentCourses(Assignment assignment)
        {
            assignment.Courses.Clear();
            DbContext.SaveChanges();
        } 
        
        public void ClearAssignmentStudents(Assignment assignment)
        {
            assignment.Students.Clear();
            DbContext.SaveChanges();
        }

        public void AttachAssignmentCourses(Assignment assignment)
        {
            DbContext.AssignmentsDbSet.Attach(assignment);
            DbContext.Entry(assignment).Collection("Courses").Load();
        }

        public void AttachAssignmentStudents(Assignment assignment)
        {
            DbContext.AssignmentsDbSet.Attach(assignment);
            DbContext.Entry(assignment).Collection("Students").Load();
        }
    }
}