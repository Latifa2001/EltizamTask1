using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Data;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Authorization;

namespace EFCoreCodeFirstSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EFCoreCodeFirstSampleContext _db; 
        public DepartmentController(IConfiguration configuration, EFCoreCodeFirstSampleContext db)
        {
            _configuration = configuration;
            _db = db;
        }
        [Authorize]
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_db.Departments);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {

            _db.Departments.Add(dep);
            _db.SaveChanges();

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            var query = _db.Departments.Find(dep.DepartmentId);
            if (query == null)
                return new JsonResult("Not Found");
            query.DepartmentName = dep.DepartmentName;
            _db.SaveChanges();
            return new JsonResult("Updated Successfully");
        }
        
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var query = _db.Departments.Find(id);
            if (query == null)
                return new JsonResult("Not Found");
            _db.Departments.Remove(query);
            _db.SaveChanges();

            return new JsonResult("Delete Successfully");
        }
    }
}
