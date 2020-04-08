using Cw3.DTO;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    class SQLDbService : Controller, IDbService
    {
        public bool CheckIndex(string index)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18645;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();
                try { 
                com.CommandText = "select * from student where IndexNumber = @IndexNumber ";
                com.Parameters.AddWithValue("IndexNumber", index);
                var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }catch(Exception e)
                {
                    return false;
                }
            }
        }

        public IActionResult EnrollStudent(EnrollStudentRequest esr)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18645;Integrated Security=True"))
            using (var com = new SqlCommand())
            using (var transaction = client.BeginTransaction())

            {
                client.Open();
                com.Connection = client;
                com.Transaction = transaction;
                var dr = com.ExecuteReader();

                try
                {
                    com.CommandText = "Select IdStudy from Studies where Name = @name";
                    com.Parameters.AddWithValue("name", esr.Studies);

                    if (!dr.Read())
                    {
                        dr.Close();
                        transaction.Rollback();
                        return BadRequest("W bazie nie ma studiów o podanej nazwie");
                    }

                    int idStudy = (int)dr["IdStudy"];
                    dr.Close();
                    com.CommandText = "Select e.IdEnrollment, e.Semester, e.IdStudy from Enrollment e" +
                    "Inner Join Studies st on e.IdStudy = st.idStudy" +
                    "Where e.Semester = 1 and st.Name = @name";

                    int IdEnrollment;
                    com.CommandText = "Select Max(IdEnrollment) as MaxId From Enrollment";
                    dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        IdEnrollment = 1;
                        dr.Close();
                    }
                    else
                    {
                        IdEnrollment = (int)dr["MaxId"];
                        dr.Close();
                    }
                    com.CommandText = "Insert Into Enrollment (IdEnrollment, Semester, IdStudy, StartDate) Values (IdEnrollment, 1, @IdStudy, GetDate())";
                    com.Parameters.AddWithValue("IdStudy", esr.Studies);
                    com.ExecuteNonQuery();

                    transaction.Commit();
                }
               
                catch (SqlException)
                {
                    transaction.Rollback();
                    return BadRequest();
                }
                return Ok();
            }
        }
       
        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }
    }
}