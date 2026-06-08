using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services.Enums
{
    public enum OperationType
    {
        
        CreateGroup = 1,
        GetAllGroups,
        UpdateGroup,
        DeleteGroup,
        GetGroupById,
        GetGroupsByTeacher,
        GetGroupsByRoom,
        SearchGroupsByName,

        
        CreateStudent = 9,
        GetAllStudents,
        UpdateStudent,
        DeleteStudent,
        GetStudentById,
        GetStudentsByAge,
        GetStudentsByGroupId,
        SearchStudentsByNameOrSurname,

        
        Exit = 0
    }
}
