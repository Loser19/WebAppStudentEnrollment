using System.ComponentModel.DataAnnotations;

namespace WebApStudentEnrolment.Models
{
    public class Course
    {
        [Key]
        public int Id { get;set; }

        [Required]
        [Display (Name = "Course Title")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int Credits { get; set; }
        public Course() { }
        public Course(int id, string name, string description, int credits)
        {
            Id = id;
            Name = name;
            Description = description;
            Credits = credits;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, Credits: {Credits}";
        }
    }
}
