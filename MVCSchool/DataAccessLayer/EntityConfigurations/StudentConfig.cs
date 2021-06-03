using System.Data.Entity.ModelConfiguration;
using MVCSchool.Models;

namespace MVCSchool.DataAccessLayer.EntityConfigurations
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {

            //StudentsAssignments Table Config
            HasMany(s => s.Assignments)
                .WithMany(a => a.Students)
                .Map(m =>
                {
                    m.ToTable("StudentsAssignments");
                    m.MapLeftKey("StudentId");
                    m.MapRightKey("AssignmentId");
                });


            //Student Table Config
            Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.DateOfBirth)
                .IsRequired()
                .HasColumnType("Date");

            Property(s => s.TuitionFee)
                .IsRequired();
        }
    }
}