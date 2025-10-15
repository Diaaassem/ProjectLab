using System.ComponentModel.DataAnnotations;

namespace ProjectLab.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Image { get; set; }
        [DataType(DataType.Date)]
        public DateOnly HireDate { get; set; }
        public string Address { get; set; }
        public int DeptId { get; set; }
        public Department Department { get; set; }
        public ICollection<InstructorCourse> TeachCourses { get; set; } = new List<InstructorCourse>();

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Age: {Age}, Salary: {Salary}, Image: {Image}, HireDate: {HireDate}, Address: {Address}, DeptId: {DeptId}";
        }
    }
}
