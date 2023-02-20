using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace StudentDataAccess
{
    public static class StudentDataAccess
    {

        public static void UpdateStudent(int studentId, string studentFname, string studentLname)
        {
                string connStr = "Data Source=JOHALSANDEEP\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string sql = "UPDATE STUDENTS SET FirstName='"+@studentFname+"',LastName='"+@studentLname+"' WHERE ID='"+@studentId+"' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@studentFname", studentFname);
            cmd.Parameters.AddWithValue("@studentLname", studentLname);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.ExecuteNonQuery();
                conn.Close();
            
            
        }
        public static void DeleteStudent(int id)
        {
            
                string connStr = "Data Source=JOHALSANDEEP\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string sql = "DELETE FROM students WHERE ID='"+ @id + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
                conn.Close();
            
           
        }
        public static void CreateStudent<T>(T obj)
        {
            
                string connStr = "Data Source=JOHALSANDEEP\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string firstName = obj.GetType().GetProperty("FirstName").GetValue(obj, null).ToString();
                string lastName = obj.GetType().GetProperty("LastName").GetValue(obj, null).ToString();
                string sql = "INSERT INTO STUDENTS(FirstName,LastName) VALUES('" + @firstName + "','" + @lastName + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.ExecuteNonQuery();
                conn.Close();
            
        }
        public static List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
           
                string connStr = "Data Source=JOHALSANDEEP\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True";
               
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string sql = "select * from students";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student() { ID = Convert.ToInt32(reader[0]), FirstName = reader[1].ToString(), LastName = reader[2].ToString() });
                }
                reader.Close();
                conn.Close();
                return students;
           
        }
    }
}
