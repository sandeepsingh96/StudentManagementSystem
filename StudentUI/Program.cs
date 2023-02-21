using StudentBusinessLogic;
using StudentDataAccess;

namespace StudentUI
{
    internal class Program
    {
        static void Main()
        {
            void getStudents()
            {
                List<Student> students = StudentBusineeLogic.GetAllStudents();
                foreach (Student student in students)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} ({student.ID})");
                }
            }
            int option;
            void Options()
            {
                Console.WriteLine("Please select any option");
                Console.WriteLine("Press 1 to add Student");
                Console.WriteLine("Press 2 to delete Student");
                Console.WriteLine("Press 3 to update Student");
                Console.WriteLine("Press 4 to get Students");
                Console.WriteLine("Press 5 to exit application");
                option = int.Parse(Console.ReadLine());
            }

            do
            {
                Options();


                switch (option)
                {
                    case 1:

                        Console.WriteLine("Enter First Name:");
                        string FirstName = Console.ReadLine();
                        Console.WriteLine("Enter Last Name:");
                        string LastName = Console.ReadLine();

                        Student s1 = new Student();
                        s1.FirstName = FirstName;
                        s1.LastName = LastName;

                        StudentBusineeLogic.AddStudent(s1);

                        getStudents();
                      

                        break;

                    case 2:

                        Console.WriteLine("Enter ID to delete:");
                        int takenID = int.Parse(Console.ReadLine());

                        StudentBusineeLogic.DeleteStudent(takenID);
                        getStudents();
                       

                        break;
                    case 3:
                        getStudents();
                        Console.WriteLine("Enter ID to update student details:");
                        int uid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter First Name:");
                        string firstNameU = Console.ReadLine();
                        Console.WriteLine("Enter Last Name:");
                        string lastNameU = Console.ReadLine();

                        Student s2 = new Student();
                        s2.FirstName = firstNameU;
                        s2.LastName = lastNameU;

                        StudentBusineeLogic.UpdateStudent(uid,s2);

                        getStudents();
                      
                        break;
                    case 4:
                        getStudents();
                     

                        break;



                    default:
                        Console.Clear();
                        
                        

                        break;
                }
            }while(option!=5);



            Console.WriteLine("thank you");
            System.Environment.Exit(0);




        }
    }
}