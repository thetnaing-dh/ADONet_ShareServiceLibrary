using ADONet.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONet_ShareService
{
    public class Program
    {
       
        private static readonly AdoDotNetService _service;
        private static readonly string _connectionString = "Server=.;Database=StudentDB;User Id=sa;Password=23032106;TrustServerCertificate=True;";
        static Program()
        {
            _service = new AdoDotNetService(_connectionString);
        }

        public static void Main(string[] args)
        {
            Console.Write("Menu - R : Read Student, C : Create Student, U : Update Student, D : Delete Student : ");
            string input = Console.ReadLine().ToUpper();
            while (input == "R" || input == "C" || input == "U" || input == "D")
            {
                switch (input) {
                    case "R":
                        GetStudents();
                        break;
                    case "C":
                        InsertStudent();
                        break;
                    case "U":
                        UpdateStudent();
                        break;
                    case "D":
                        DeleteStudent();
                        break;
                    default:
                        break;
                }
                Console.Write("Menu - R : Read Student, C : Create Student, U : Update Student, D : Delete Student : ");
                input = Console.ReadLine().ToUpper();
            }
        }

        private static void GetStudents()
        {
            string query = @"SELECT * FROM Tbl_Student WHERE DeleteFlag = 0;";
            DataTable dt = _service.Query(query);
            Console.WriteLine("Student List : ");
            foreach (DataRow dr in dt.Rows) { 
                Console.WriteLine($@"ID : {dr["Id"]}, RollNo : {dr["RollNo"]}, Name : {dr["Name"]}, Email : {dr["Email"]}");
            }
        }

        private static void InsertStudent()
        {
            Console.Write("Enter RollNo : ");
            string RollNo = Console.ReadLine();

            Console.Write("Enter Student Name : ");
            string Name = Console.ReadLine();

            Console.Write("Enter Email : ");
            string Email = Console.ReadLine();

            string query = @"INSERT INTO Tbl_Student(RollNo,Name,Email,DeleteFlag) 
                            VALUES(@RollNo,@Name,@Email,0);";
            DataTable dt = _service.Query(query, new SqlParameterModel
            {
                Name = "@RollNo",
                Value = RollNo
            }, new SqlParameterModel
            {
                Name = "@Name",
                Value = Name
            }, new SqlParameterModel
            {
                Name = "@Email",
                Value = Email
            });

            if (dt != null) {
                Console.WriteLine("Inserting Successfully.");
            }
            else
            {
                Console.WriteLine("Insertion Failed.");
            }
        }

        private static void UpdateStudent()
        {
            Console.Write("Enter Student Id to Edit : ");
            int id = int.TryParse(Console.ReadLine(), out var tempid) ? tempid : 0;

            if (id == 0)
            {
                Console.WriteLine("Invalid Student Id.");
                return;
            }

            string checkExist = @"SELECT Id FROM Tbl_Student WHERE Id = @Id and DeleteFlag = 0;";
            DataTable dtExist = _service.Query(checkExist, new SqlParameterModel
            {
                Name = "@Id",
                Value = id.ToString()
            });

            Console.WriteLine(dtExist);

            if (dtExist.Rows.Count == 0) {
                Console.WriteLine("Student is Not Found or Deleted.");
                return;
            }

            Console.Write("Enter RollNo : ");
            string RollNo = Console.ReadLine();

            Console.Write("Enter Student Name : ");
            string Name = Console.ReadLine();

            Console.Write("Enter Email : ");
            string Email = Console.ReadLine();

            string query = @"UPDATE Tbl_Student SET 
                            RollNo = @RollNo 
                            ,Name = @Name 
                            ,Email = @Email 
                            WHERE Id = @Id";
            DataTable dt = _service.Query(query, new SqlParameterModel
            {
                Name = "@RollNo",
                Value = RollNo
            }, new SqlParameterModel
            {
                Name = "@Name",
                Value = Name
            }, new SqlParameterModel
            {
                Name = "@Email",
                Value = Email
            }, new SqlParameterModel
            {
                Name = "@Id",
                Value = id.ToString()
            });

            if (dt != null)
            {
                Console.WriteLine("Updating Successfully.");
            }
            else
            {
                Console.WriteLine("Updation Failed.");
            }
        }

        private static void DeleteStudent()
        {
            Console.Write("Enter Student Id to Edit : ");
            int id = int.TryParse(Console.ReadLine(), out var tempid) ? tempid : 0;

            if (id == 0)
            {
                Console.WriteLine("Invalid Student Id.");
                return;
            }

            string checkExist = @"SELECT Id FROM Tbl_Student WHERE Id = @Id and DeleteFlag = 0;";
            DataTable dtExist = _service.Query(checkExist, new SqlParameterModel
            {
                Name = "@Id",
                Value = id.ToString()
            });

            Console.WriteLine(dtExist);

            if (dtExist.Rows.Count == 0)
            {
                Console.WriteLine("Student is Not Found or Deleted.");
                return;
            }         

            string query = @"UPDATE Tbl_Student SET 
                            DeleteFlag = 1 
                            WHERE Id = @Id";
            DataTable dt = _service.Query(query, new SqlParameterModel           
            {
                Name = "@Id",
                Value = id.ToString()
            });

            if (dt != null)
            {
                Console.WriteLine("Deleting Successfully.");
            }
            else
            {
                Console.WriteLine("Deletion Failed.");
            }
        }
    }
}