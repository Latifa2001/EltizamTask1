using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Data;
using System.Runtime.Intrinsics.Arm;
using EFCoreCodeFirstSample.IRepository;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using EFCoreCodeFirstSample.Core.DTOs;
using AutoMapper;

namespace EFCoreCodeFirstSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly EFCoreCodeFirstSampleContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env, EFCoreCodeFirstSampleContext db, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _env = env;
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Route("GetAllEmployees")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var model = await _unitOfWork.Employees.GetAll();
            return Ok(model);
        }        
        [Route("GetEmployeeById")]
        [HttpPost]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var model = await _unitOfWork.Employees.Get(x => x.EmployeeId == id);
            return Ok(model);
        }
        [Route("GetDepartment")]
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            var model = await _unitOfWork.Departments.GetAll();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostCountry([FromBody] LoginEmployeeDTO empDTO)
        {
            var emp = _mapper.Map<Employee>(empDTO);
             await _unitOfWork.Employees.Insert(emp);
            await _unitOfWork.Save();
            return Ok("Added Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateEmployeeDTO empDTO)
        {
            var emp = await _unitOfWork.Employees.Get(x => x.EmployeeId == empDTO.EmployeeId);
            _mapper.Map(empDTO,emp);
             _unitOfWork.Employees.Update(emp);
            await _unitOfWork.Save();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Employees.Delete(id);
            await _unitOfWork.Save();
            return Ok("Delete Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            //auth2 -> token jason web token idenetity.
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
                
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
//{
//    "employeeName": "eee",
//  "department": "IT",
//  "photoFileName": "dd.png",
//  "dateOfJoining": "2-2-2001",
//  "employeeId": 4
//}