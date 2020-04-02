using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = new List<Student>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18645;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "Select FirstName, LastName, IndexNumber, Name, Semester from Student " +
                    "Inner Join Enrollment on Enrollment.IdEnrollment = Student.IdEnrollment " +
                    "Inner Join Studies on Studies.IdStudy = Enrollment.IdStudy";

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.Studies_name = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    students.Add(st);
                }
            }
            return Ok(students);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudents(String id)
        {
            var enrollmentList = new List<Enrollment>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18645;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;

                com.CommandText = "Select Name, Semester, StartDate from Student " +
                "Inner Join Enrollment on Student.IdEnrollment = Enrollment.IdEnrollment " +
                "Inner Join Studies on Enrollment.IdStudy = Studies.IdStudy " +
                "Where Student.IndexNumber=@id";

                com.Parameters.AddWithValue("id", id);

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var enr = new Enrollment();
                    enr.Name = dr["Name"].ToString();
                    enr.Semester = dr["Semester"].ToString();
                    enr.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                    enrollmentList.Add(enr);
                }
            }
            return Ok(enrollmentList);
        }


        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            //....
            return Ok(student);
        }

        [HttpPut("{id:int}")]
        public IActionResult PutStudent(int id)
        {
            return Ok($"Aktualizacja ukonczona {id}");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok($"Usuwanie ukonczone {id}");
        }
    }
}