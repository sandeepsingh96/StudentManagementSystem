using StudentDataAccess;
using System.Data.SqlClient;
namespace StudentBusinessLogic
{
    public static class StudentBusineeLogic
    {
        public static List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            try
            {
                students = StudentDataAccess.StudentDataAccess.GetAllRecords<Student>();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while retrieving the students: " + ex.Message);
            }

            return students;
        }

        public static void AddStudent(Student student)
        {
            // Check for empty values
            if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName))
            {
                Console.WriteLine("First and last name cannot be empty.");
                return;
                
            }

            // Add any additional validation checks here

            try
            {
                StudentDataAccess.StudentDataAccess.CreateRecord(student);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine("Error adding student: " + ex.Message);
                // Rethrow the exception to be handled by the calling code
                throw;
            }
        }

        public static void DeleteStudent(int id)
        {
            if (id <= 0)
            {
                Console.WriteLine("Invalid student ID.");
                return;
                
            }

            try
            {
                List<Student> students = StudentDataAccess.StudentDataAccess.GetAllRecords<Student>();
                Student studentToDelete = students.FirstOrDefault(s => s.ID == id);
                if (studentToDelete == null)
                {
                    Console.WriteLine("The student with ID " + id + " does not exist.");
                    return;
                }

                StudentDataAccess.StudentDataAccess.DeleteRecord<Student>(id);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine("Error deleting student: " + ex.Message);
                // Rethrow the exception to be handled by the calling code
                throw;
            }
        }

        public static void UpdateStudent(int id, Student student)
        {
            if (id <= 0)
            {
                Console.WriteLine("Invalid student ID.");
                return;
            }
            try
            {
                // Check if the student exists before updating
                var existingStudent = StudentDataAccess.StudentDataAccess.GetRecordById<Student>(id);
                if (existingStudent == null)
                {
                    Console.WriteLine($"Student with ID {id} does not exist.");
                    return;
                }
                if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName))
                {
                    Console.WriteLine("First and last name cannot be empty.");
                }

                else { StudentDataAccess.StudentDataAccess.UpdateRecord(id, student); }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine("Error updating student: " + ex.Message);
                // Rethrow the exception to be handled by the calling code
                throw;
            }
            

            
        }
        





    }
}