namespace ProjectLab.Models
{
    public class Student
    {
        public int SSN { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int? DeptId { get; set; }
        public Department Department { get; set; }
        public ICollection<StudentCourse> Registrations { get; set; } = new List<StudentCourse>();

        public override string ToString()
        {
            return $"SSN: {SSN}, Name: {Name}, Age: {Age}, Image: {Image}, Address: {Address}, Email: {Email}";
        }
    }
}
