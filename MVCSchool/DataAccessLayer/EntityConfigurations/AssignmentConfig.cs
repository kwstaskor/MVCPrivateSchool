using System.Data.Entity.ModelConfiguration;
using MVCSchool.Models;

namespace MVCSchool.DataAccessLayer.EntityConfigurations
{
    public class AssignmentConfig : EntityTypeConfiguration<Assignment>
    {
        public AssignmentConfig()
        {
            Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Submission)
                .IsRequired();

        }
    }
}