using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Enrollment
    {
        public int IdEnrollment { get; set; }
        public string Semester { get; set; }
        public string IdStudy { get; set; }
        public DateTime StartDate { get; set; }
        public string Name { get; set; }
    }
}