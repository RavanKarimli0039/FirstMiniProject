using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string TeacherFullName { get; set; }
        public string RoomName { get; set; }
    }
}
