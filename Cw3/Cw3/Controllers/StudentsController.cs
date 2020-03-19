using System;
using System.Collections.Generic;
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

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderby)
        {
            return Ok(_dbService.GetStudents());
        }


        //QueryString
        [HttpGet]
        public string GetStud(string orderBy)
        {
            var s = HttpContext.Request;

            return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";
        }

        //Url segment
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }else if(id == 2)
            {
                return Ok("Malewski");
            }

            return NotFound("Nie znaleziono studenta");
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