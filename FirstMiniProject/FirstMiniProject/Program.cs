using Project.Controllers;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services;
using Service.Services.Enums;
using Service.Services.Interfaces;
using System.Text;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Azərbaycan şriftlərinin düzgün görünməsi üçün
            Console.OutputEncoding = Encoding.UTF8;

            
            IGroupRepository groupRepo = new GroupRepository();
            IStudentRepository studentRepo = new StudentRepository();

            IGroupService groupService = new GroupService(groupRepo, studentRepo);
            IStudentService studentService = new StudentService(studentRepo, groupRepo);

            GroupController groupController = new GroupController(groupService);
            StudentController studentController = new StudentController(studentService, groupService);

            // 2. ANA PROQRAM DÖVRÜ
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n======================================");
                Console.WriteLine("======= KURS İDARƏETMƏ SİSTEMİ =======");
                Console.WriteLine("======================================");
                Console.ResetColor();

                Console.WriteLine("\n--- QRUP ƏMƏLİYYATLARI ---");
                Console.WriteLine("1. Qrup yarat");
                Console.WriteLine("2. Bütün qrupları listələ");
                Console.WriteLine("3. Qrupu yenilə");
                Console.WriteLine("4. Qrupu sil");
                Console.WriteLine("5. ID-yə görə qrupu tap");
                Console.WriteLine("6. Müəllimə görə qrupları tap");
                Console.WriteLine("7. Otağa görə qrupları tap");
                Console.WriteLine("8. Ada görə qrupları axtar");

                Console.WriteLine("\n--- TƏLƏBƏ ƏMƏLİYYATLARI ---");
                Console.WriteLine("9. Tələbə yarat");
                Console.WriteLine("10. Bütün tələbələri listələ");
                Console.WriteLine("11. Tələbəni yenilə");
                Console.WriteLine("12. Tələbəni sil");
                Console.WriteLine("13. ID-yə görə tələbəni tap");
                Console.WriteLine("14. Yaşına görə tələbələri tap");
                Console.WriteLine("15. Qrup ID-sinə görə tələbələri tap");
                Console.WriteLine("16. Ad/Soyada görə tələbə axtar");

                Console.WriteLine("\n0. Proqramdan çıx");
                Console.WriteLine("======================================");
                Console.Write("Zəhmət olmasa seçiminizi edin (0-16): ");

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
                            Console.WriteLine("Proqramdan çıxılır... Sağ olun!");
                            return;

                        default:
                            Console.WriteLine("Xəta: Belə bir seçim yoxdur!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Xəta: Zəhmət olmasa düzgün rəqəm seçin!");
                }
            }
        }
    }
}