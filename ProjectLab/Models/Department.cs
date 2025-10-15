namespace ProjectLab.Models
{
    public class Department
    {
        public int DeptId { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public string Location { get; set; }
        public Branch Branch { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

        public override string ToString()
        {
            return $"DeptId: {DeptId}, Name: {Name}, Manager: {Manager}, Location: {Location}, Branch: {Branch}, Students: {Students.Count}, Instructors: {Instructors.Count}";
        }
    }
}