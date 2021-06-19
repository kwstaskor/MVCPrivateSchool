using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class CourseRepos : Repository<Course>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public CourseRepos(ApplicationDbContext context) : base(context)
        {
        }

        public void AssignStudentsToCourse(Course course, int[] studentIds)
        {
            if (studentIds is null) return;

            foreach (var id in studentIds)
            {
                var student = DbContext.StudentsDbSet.Find(id);
                if (!(student is null))
                {
                    course.Students.Add(student);
                }
            }

            DbContext.SaveChanges();
        }

        public void AssignTrainersToCourse(Course course, int[] trainerIds)
        {
            if (trainerIds is null) return;

            foreach (var id in trainerIds)
            {
                var trainer = DbContext.TrainersDbSet.Find(id);
                if (!(trainer is null))
                {
                    course.Trainers.Add(trainer);
                }
            }

            DbContext.SaveChanges();
        }

        public void AssignAssignmentsToCourse(Course course, int[] assignmentIds)
        {
            if (assignmentIds is null) return;

            foreach (var id in assignmentIds)
            {
                var assignment = DbContext.AssignmentsDbSet.Find(id);
                if (!(assignment is null))
                {
                    course.Assignments.Add(assignment);
                }
            }

            DbContext.SaveChanges();
        }

        public void ClearCourseStudents(Course course)
        {
            course.Students.Clear();
            DbContext.SaveChanges();
        }

        public void ClearCourseTrainers(Course course)
        {
            course.Trainers.Clear();
            DbContext.SaveChanges();
        }

        public void ClearCourseAssignments(Course course)
        {
            course.Assignments.Clear();
            DbContext.SaveChanges();
        }

        public void AttachStudentsCourse(Course course)
        {
            DbContext.CoursesDbSet.Attach(course);
            DbContext.Entry(course).Collection("Students").Load();
        } 

        public void AttachTrainersCourse(Course course)
        {
            DbContext.CoursesDbSet.Attach(course);
            DbContext.Entry(course).Collection("Trainers").Load();
        }

        public void AttachAssignmentsCourse(Course course)
        {
            DbContext.CoursesDbSet.Attach(course);
            DbContext.Entry(course).Collection("Assignments").Load();
        }
    }
}