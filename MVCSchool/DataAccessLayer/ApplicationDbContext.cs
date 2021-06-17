using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCSchool.DataAccessLayer.EntityConfigurations;
using MVCSchool.Models;

namespace MVCSchool.DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
      
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Course> CoursesDbSet { get; set; }
        public DbSet<Assignment> AssignmentsDbSet { get; set; }
        public DbSet<Trainer> TrainersDbSet { get; set; }
        public DbSet<Student> StudentsDbSet { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CourseConfig());
            modelBuilder.Configurations.Add(new AssignmentConfig());
            modelBuilder.Configurations.Add(new StudentConfig());
            modelBuilder.Configurations.Add(new TrainerConfig());
            
        }

    }
}