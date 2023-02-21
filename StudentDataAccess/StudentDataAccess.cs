using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
namespace StudentDataAccess
{
    public static class StudentDataAccess
    {

        private static string connStr = "Data Source=JOHALSANDEEP\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True;";
        //static StudentDataAccess()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("config.json", optional: true, reloadOnChange: true);

        //    IConfigurationRoot configuration = builder.Build();
        //    connStr = configuration.GetConnectionString("DefaultConnection");
        //}

        public static void UpdateRecord<T>(int id, T obj) where T : class
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                var props = obj.GetType().GetProperties().Where(p => p.Name != "ID");
                var propSetters = string.Join(",", props.Select(p => $"{p.Name} = @{p.Name}"));
                string sql = $"UPDATE {typeof(T).Name}s SET {propSetters} WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                foreach (var prop in props)
                {
                    cmd.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(obj));
                }
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteRecord<T>(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = $"DELETE FROM {typeof(T).Name}s WHERE ID=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void CreateRecord<T>(T obj) where T : class
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                var props = obj.GetType().GetProperties().Where(p => p.Name != "ID");
                var propNames = string.Join(",", props.Select(p => p.Name));
                var propValues = string.Join(",", props.Select(p => $"@{p.Name}"));
                string sql = $"INSERT INTO {typeof(T).Name}s ({propNames}) VALUES ({propValues})";
                SqlCommand cmd = new SqlCommand(sql, conn);
                foreach (var prop in props)
                {
                    cmd.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(obj));
                }
                cmd.ExecuteNonQuery();
            }
        }
        public static List<T> GetAllRecords<T>() where T : new()
        {
            List<T> records = new List<T>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = $"SELECT * FROM {typeof(T).Name}s";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    T record = new T();

                    foreach (var prop in typeof(T).GetProperties())
                    {
                        prop.SetValue(record, reader[prop.Name]);
                    }

                    records.Add(record);
                }

                reader.Close();
            }

            return records;
        }
         public static T GetRecordById<T>(int id) where T : new()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = $"SELECT * FROM  { typeof(T).Name}s WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    T record = new T();
                    PropertyInfo[] properties = typeof(T).GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        if (reader[property.Name] != DBNull.Value)
                        {
                            property.SetValue(record, reader[property.Name]);
                        }
                    }

                    reader.Close();
                    return record;
                }
                else
                {
                    reader.Close();
                    
                    return default;
                }
            }
        }
    }
}
