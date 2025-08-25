using System.ComponentModel.DataAnnotations;

namespace WebApStudentEnrolment.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        public string Address { get; set; } = string.Empty;

        public Student() { }

        public Student(int id, string name, string email, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Email: {Email}, Address: {Address}";
        }
    }
}
