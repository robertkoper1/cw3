using Cw3.DTO;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public IActionResult EnrollStudent(EnrollStudentRequest esr);

        public bool CheckIndex(string index);
     
    }
}