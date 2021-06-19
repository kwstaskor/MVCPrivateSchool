using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using MVCSchool.Models.Validations;

namespace MVCSchool.Models
{
    [Validator(typeof(TrainerValidate))]
    public class Trainer
    {
        public Trainer()
        {
            Courses = new HashSet<Course>();
        }
        public int TrainerId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Years Of Experience")]
        public string YearsOfExperience { get; set; }

        [Display(Name = "About")]
        public string Bio { get; set; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Teaching Subject")]
        public string Subject { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}