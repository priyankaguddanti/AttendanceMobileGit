using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Models
{
    public class ClassViewDetailsRequest
    {
        public string UserName { get; set; }

        public ClassViewRange Range { get; set; }
    }
}
