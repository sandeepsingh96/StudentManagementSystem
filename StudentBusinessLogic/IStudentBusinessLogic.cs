using StudentDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace StudentBusinessLogic
{ 
    public interface IStudentBusinessLogic
    {
        public static abstract List<Student> GetAllStudents();
        public static abstract void AddStudent( Student student );
        public static abstract void DeleteStudent(int value);
        public static abstract void UpdateStudent(int value, Student student);


    }
}