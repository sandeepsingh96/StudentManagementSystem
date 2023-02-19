using StudentDataAccess;

namespace StudentBusinessLogic
{
    public static class StudentBusineeLogic
    {
        public static List<Student> GetAllStudents()
        {
            return StudentDataAccess.StudentDataAccess.GetAllStudents();
        }

        public static void AddStudent(Student student)
        {
            StudentDataAccess.StudentDataAccess.CreateStudent(student);
        }

        public static void DeleteStudent(int id)
        {
            StudentDataAccess.StudentDataAccess.DeleteStudent(id);
        }

        public static void UpdateStudent(int id, string firstName, string lastName)
        {
            StudentDataAccess.StudentDataAccess.UpdateStudent(id, firstName, lastName);
        }
    }
}