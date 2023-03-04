using System.Collections.Generic;
namespace StudentDataAccess
{
    public interface IStudentDataAccess
    {
        void CreateRecord<T>(T obj) where T : class;
        void UpdateRecord<T>(int id, T obj) where T : class;
        void DeleteRecord<T>(int id);
        List<T> GetAllRecords<T>() where T : new();
        T GetRecordById<T>(int id) where T : new();
    }
}