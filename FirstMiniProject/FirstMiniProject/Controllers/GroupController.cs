using Domain.Entities;
using Service.Exceptions;
using Service.Services.Interfaces;

namespace Project.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }



        public void Create()
        {
            Console.WriteLine("--- Yeni Qrup Yaradılması ---");


            string name;
            while (true)
            {
                Console.Write("Qrup adını daxil edin: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Xəta: Qrup adı boş ola bilməz!");
                    continue;
                }


                if (name.All(char.IsDigit))
                {
                    Console.WriteLine("Xəta: Qrup adı yalnız rəqəmlərdən ibarət ola bilməz! Adın daxilində ən azı bir hərf və ya simvol olmalıdır.");
                    continue;
                }


                var allGroups = _groupService.GetAll();
                bool isDuplicate = allGroups.Exists(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    Console.WriteLine("Xəta: Bu adda qrup artıq mövcuddur! Zəhmət olmasa başqa ad daxil edin.");
                    continue;
                }

                break;
            }


            string teacher;
            while (true)
            {
                Console.Write("Müəllim adını (FullName) daxil edin: ");
                teacher = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(teacher))
                {
                    Console.WriteLine("Xəta: Müəllim adı boş ola bilməz!");
                    continue;
                }


                bool hasDigit = teacher.Any(char.IsDigit);
                if (hasDigit)
                {
                    Console.WriteLine("Xəta: Müəllim adında rəqəm ola bilməz!");
                    continue;
                }
                break;
            }


            string room;
            while (true)
            {
                Console.Write("Otaq adını daxil edin: ");
                room = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(room))
                {
                    Console.WriteLine("Xəta: Otaq adı boş ola bilməz!");
                    continue;
                }
                break;
            }
            try
            {
                Group newGroup = new Group
                {
                    Name = name,
                    TeacherFullName = teacher,
                    RoomName = room
                };

                _groupService.Create(newGroup);
                Console.WriteLine("Uğurla yaradıldı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gözlənilməz xəta: {ex.Message}");
            }
        }

        public void Update()
        {
            Console.WriteLine("--- Qrup Yeniləmə ---");

            int id;
            while (true)
            {
                Console.Write("Yeniləmək istədiyiniz qruun ID-sini daxil edin: ");
                if (int.TryParse(Console.ReadLine(), out id)) break;
                Console.WriteLine("Xəta: ID ancaq rəqəm ola bilər!");
            }

            Console.Write("Yeni qrup adı (dəyişmək istəmirsinizsə boş buraxın): ");
            string newName = Console.ReadLine();


            string newTeacher;
            while (true)
            {
                Console.Write("Yeni müəllim adı (dəyişmək istəmirsinizsə boş buraxın): ");
                newTeacher = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newTeacher))
                {
                    break;
                }


                if (newTeacher.Any(char.IsDigit))
                {
                    Console.WriteLine("Xəta: Müəllim adında rəqəm ola bilməz!");
                    continue;
                }

                break;
            }

            Console.Write("Yeni otaq adı (dəyişmək istəmirsinizsə boş buraxın): ");
            string newRoom = Console.ReadLine();

            try
            {
                Group updatedData = new Group
                {
                    Name = newName,
                    TeacherFullName = newTeacher,
                    RoomName = newRoom
                };

                _groupService.Update(id, updatedData);
                Console.WriteLine("Məlumatlar yeniləndi.");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta baş verdi: {ex.Message}");
            }
        }

        public void GetById()
        {
            while (true)
            {
                Console.Write("Axtardığınız ID-ni daxil edin: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Xəta: ID rəqəm olmalıdır!");
                    continue;
                }

                try
                {
                    var group = _groupService.GetById(id);

                    if (group == null)
                    {
                        throw new NotFoundException($"Xəta: {id} ID-li qrup tapılmadı!");
                    }

                    Console.WriteLine($"ID: {group.Id} | Ad: {group.Name} | Müəllim: {group.TeacherFullName} | Otaq: {group.RoomName}");

                    break;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Delete()
        {
            while (true)
            {
                Console.Write("Silmək istədiyiniz qrupun ID-sini daxil edin: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Xəta: ID rəqəm olmalıdır!");
                    continue;
                }

                try
                {
                    _groupService.Delete(id);
                    Console.WriteLine("Qrup uğurla silindi.");

                    break;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Gözlənilməz xəta baş verdi: {ex.Message}");

                }
            }
        }

        public void GetAll()
        {
            var groups = _groupService.GetAll();

            if (groups.Count == 0)
            {
                Console.WriteLine("Sistemdə hələ heç bir qrup yoxdur.");
                return;
            }

            Console.WriteLine("\n--- Mövcud Qruplar ---");
            foreach (var g in groups)
            {
                Console.WriteLine($"ID: {g.Id} | Ad: {g.Name} | Müəllim: {g.TeacherFullName} | Otaq: {g.RoomName}");
            }
        }

        public void GetAllByTeacher()
        {
            Console.Write("Axtardığınız müəllim adını daxil edin: ");
            string teacherName = Console.ReadLine();

            var groups = _groupService.GetAllByTeacher(teacherName);

            if (groups.Count == 0)
            {
                Console.WriteLine("Bu adda müəllimi olan qrup tapılmadı.");
                return;
            }

            foreach (var item in groups)
            {
                Console.WriteLine($"ID: {item.Id} | Ad: {item.Name} | Müəllim: {item.TeacherFullName}");
            }
        }

        public void SearchByName()
        {
            Console.Write("Axtardığınız qrup adını (və ya bir hissəsini) daxil edin: ");
            string name = Console.ReadLine();

            var groups = _groupService.SearchByName(name);

            if (groups.Count == 0)
            {
                Console.WriteLine("Axtarışa uyğun qrup tapılmadı.");
                return;
            }

            foreach (var item in groups)
            {
                Console.WriteLine($"ID: {item.Id} | Ad: {item.Name} | Otaq: {item.RoomName}");
            }
        }

        public void GetAllByRoom()
        {
            Console.Write("Axtardığınız otaq adını daxil edin: ");
            string roomName = Console.ReadLine();

            var groups = _groupService.GetAllByRoom(roomName);

            if (groups.Count == 0)
            {
                Console.WriteLine("Bu otaqda heç bir qrup tapılmadı.");
                return;
            }

            foreach (var item in groups)
            {
                Console.WriteLine($"ID: {item.Id} | Ad: {item.Name} | Müəllim: {item.TeacherFullName} | Otaq: {item.RoomName}");
            }
        }
    }
}