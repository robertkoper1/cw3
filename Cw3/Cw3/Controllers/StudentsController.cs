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
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18645;Integrated Security=True";

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            var listStudents = new List<Student>();
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student";

                con.Open();
                SqlDataReader dataReader = com.ExecuteReader();

                while (dataReader.Read())
                {
                    var st = new Student();

                    if (dataReader["IndexNumber"] != DBNull.Value)
                        st.IndexNumber = dataReader["IndexNumber"].ToString();

                    if (dataReader["FirstName"] != DBNull.Value)
                        st.IndexNumber = dataReader["FirstName"].ToString();

                    if (dataReader["LastName"] != DBNull.Value)
                        st.IndexNumber = dataReader["LastName"].ToString();

                    if (dataReader["BirthDate"] != DBNull.Value)
                        st.IndexNumber = dataReader["BirthDate"].ToString();

                    if (dataReader["IdEnrollment"] != DBNull.Value)
                        st.IndexNumber = dataReader["IdEnrollment"].ToString();

                    listStudents.Add(st);
                }
            }

                return Ok(listStudents);
        }


        [HttpGet("{indexNumber}")]
        public IActionResult GetStudents(string indexNumber)
        {


            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student where IndexNumber=@IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", indexNumber);


                con.Open();
                var dataReader = com.ExecuteReader();
                if (dataReader.Read())
                {

                    var st = new Student();

                    if (dataReader["IndexNumber"] != DBNull.Value)
                        st.IndexNumber = dataReader["IndexNumber"].ToString();

                    if (dataReader["FirstName"] != DBNull.Value)
                        st.FirstName = dataReader["FirstName"].ToString();

                    if (dataReader["LastName"] != DBNull.Value)
                        st.LastName = dataReader["LastName"].ToString();

                    if (dataReader["BirthDate"] != DBNull.Value)
                        st.BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString());

                    if (dataReader["IdEnrollment"] != DBNull.Value)
                        st.IdEnrollment = int.Parse(dataReader["IdEnrollment"].ToString());

                    return Ok(st);
                }

            }
            return NotFound("Student nie znaleziony");
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