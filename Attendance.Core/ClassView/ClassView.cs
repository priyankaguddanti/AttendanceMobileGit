using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.ClassView
{
    public class ClassView
    {
        public string CourseTitle { get; set; }

        public string CourseAttendedDate { get; set; }

        public string Status { get; set; }

        public bool CanBeDisputed { get; set; }

        public int CourseAttendanceId { get; set; }
    }
}
