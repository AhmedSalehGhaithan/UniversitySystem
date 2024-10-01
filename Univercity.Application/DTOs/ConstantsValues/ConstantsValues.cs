using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Application.DTOs.ConstantsValues
{
    public static class ConstantsValues
    {
        public class CachingKeys 
        {
            public const string GetAllMajorKey = "ConstantsValues.CachingKeys.GetAllStudentsName";
            public const string GetAllTeachersKey = "ConstantsValues.CachingKeys.GetAllTeachersKey";
            public const string GetAllStudentsName = "ConstantsValues.CachingKeys.GetAllStudentsName";
            public const string GetAllSubjectKey = "ConstantsValues.CachingKeys.GetAllSubjectKey";
        }
        public class ObjectType
        {
            public const string Major = "Major";
            public const string Student = "Student";
            public const string Subject = "Subject";
            public const string Teacher = "Teacher";
        }
    }
}
