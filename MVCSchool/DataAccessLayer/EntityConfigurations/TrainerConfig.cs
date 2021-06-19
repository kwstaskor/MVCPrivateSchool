using System.Data.Entity.ModelConfiguration;
using MVCSchool.Models;

namespace MVCSchool.DataAccessLayer.EntityConfigurations
{
    public class TrainerConfig :EntityTypeConfiguration<Trainer>
    {
        public TrainerConfig()
        {
            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.YearsOfExperience)
                .IsRequired()
                .HasMaxLength(5);

            Property(t => t.Bio)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}