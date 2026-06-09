using System;
using System.Text;
using FirstMiniProject.Controllers;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services;
using Service.Services.Interfaces;
using Service.Services.Enums;

namespace FirstMiniProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IGroupRepository groupRepo = new GroupRepository();
            IStudentRepository studentRepo = new StudentRepository();

            IGroupService groupService = new GroupService(groupRepo, studentRepo);
            IStudentService studentService = new StudentService(studentRepo, groupRepo);

            GroupController groupController = new GroupController(groupService);
            StudentController studentController = new StudentController(studentService, groupService);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n======================================");
                Console.WriteLine("======= COURSE MANAGEMENT SYSTEM =======");
                Console.WriteLine("======================================");
                Console.ResetColor();

                Console.WriteLine("\n--- GROUP OPERATIONS ---");
                Console.WriteLine("1. Create Group");
                Console.WriteLine("2. List All Groups");
                Console.WriteLine("3. Update Group");
                Console.WriteLine("4. Delete Group");
                Console.WriteLine("5. Get Group By ID");
                Console.WriteLine("6. Get Groups By Teacher");
                Console.WriteLine("7. Get Groups By Room");
                Console.WriteLine("8. Search Groups By Name");

                Console.WriteLine("\n--- STUDENT OPERATIONS ---");
                Console.WriteLine("9. Create Student");
                Console.WriteLine("10. List All Students");
                Console.WriteLine("11. Update Student");
                Console.WriteLine("12. Delete Student");
                Console.WriteLine("13. Get Student By ID");
                Console.WriteLine("14. Get Students By Age");
                Console.WriteLine("15. Get Students By Group ID");
                Console.WriteLine("16. Search Student By Name/Surname");

                Console.WriteLine("\n0. Exit");
                Console.WriteLine("======================================");
                Console.Write("Please select an option (0-16): ");

                string input = Console.ReadLine();

                if (Enum.TryParse(input, out OperationType choice))
                {
                    switch (choice)
                    {
                        case OperationType.CreateGroup:
                            groupController.Create();
                            break;
                        case OperationType.GetAllGroups:
                            groupController.GetAll();
                            break;
                        case OperationType.UpdateGroup:
                            groupController.Update();
                            break;
                        case OperationType.DeleteGroup:
                            groupController.Delete();
                            break;
                        case OperationType.GetGroupById:
                            groupController.GetById();
                            break;
                        case OperationType.GetGroupsByTeacher:
                            groupController.GetAllByTeacher();
                            break;
                        case OperationType.GetGroupsByRoom:
                            groupController.GetAllByRoom();
                            break;
                        case OperationType.SearchGroupsByName:
                            groupController.SearchByName();
                            break;

                        case OperationType.CreateStudent:
                            studentController.Create();
                            break;
                        case OperationType.GetAllStudents:
                            studentController.GetAll();
                            break;
                        case OperationType.UpdateStudent:
                            studentController.Update();
                            break;
                        case OperationType.DeleteStudent:
                            studentController.Delete();
                            break;
                        case OperationType.GetStudentById:
                            studentController.GetById();
                            break;
                        case OperationType.GetStudentsByAge:
                            studentController.GetAllByAge();
                            break;
                        case OperationType.GetStudentsByGroupId:
                            studentController.GetStudentsByGroupId();
                            break;
                        case OperationType.SearchStudentsByNameOrSurname:
                            studentController.SearchByNameOrSurname();
                            break;

                        case OperationType.Exit:
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection!");
                }
            }
        }
    }
}