using System.Data.Entity.ModelConfiguration;
using MVCSchool.Models;

namespace MVCSchool.DataAccessLayer.EntityConfigurations
{
    public class CourseConfig : EntityTypeConfiguration<Course>
    {
        public CourseConfig()
        {
            //StudentsCourses Table Config
            HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .Map(m =>
                {
                    m.ToTable("StudentsCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("StudentId");
                });


            //AssignmentsCourses Table Config
            HasMany(c => c.Assignments)
                .WithMany(a => a.Courses)
                .Map(m =>
                {
                    m.ToTable("AssignmentsCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("AssignmentId");
                });


            //TrainersCourses Table Config
            HasMany(c => c.Trainers)
                .WithMany(t => t.Courses)
                .Map(m =>
                {
                    m.ToTable("TrainersCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("TrainerId");
                });


            //Course Table Config
            Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);


            Property(c => c.Stream)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.StartDate)
                .IsRequired()
                .HasColumnType("Date");

            Property(c => c.EndDate)
                .IsRequired()
                .HasColumnType("Date");

        }
    }
}