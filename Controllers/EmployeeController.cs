using HRISSystemBackend.Data;
using HRISSystemBackend.Entities;
using HRISSystemBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace HRISSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<DataTableResponse>> Get()
        {
            //var emps = await _context.Employees.Include(obj => obj.Position).ToListAsync();
            var emps = (from emp in _context.Employees
                        join pos in _context.Positions on emp.job_position equals pos.id
                        join man in _context.Employees on emp.manager equals man.emp_code
                        into emm
                        from subgroup in emm.DefaultIfEmpty()
                        select new
                        {
                            emp_code = emp.emp_code,
                            name = emp.name,
                            employment_type = emp.employment_type,
                            position_name = pos.name,
                            manager = emp.manager,
                            manager_name = subgroup.name,
                            last_updated = emp.last_updated.ToString("dd MMMM yyyy")
                        }).ToList();


            return new DataTableResponse
            {
                RecordsTotal = emps.Count(),
                RecordsFiltered = 10,
                Data = emps.ToArray()
            };
        }

        [HttpGet("{empcode}")]

        public async Task<ActionResult<DataTableResponse>> Get(string empcode)
        {
            if (empcode == null)
            {
                return BadRequest("Employee not found");
            }


            var employee = (from emp in _context.Employees
                            join pos in _context.Positions on emp.job_position equals pos.id
                            join man in _context.Employees on emp.manager equals man.emp_code
                            into emm
                            from subgroup in emm.DefaultIfEmpty()
                            select new
                            {
                                emp_code = emp.emp_code,
                                name = emp.name,
                                address = emp.address,
                                phone = emp.phone,
                                email = emp.email,                                
                                join_date = emp.join_date,                                
                                employment_type = emp.employment_type,
                                position_name = pos.name,
                                job_position = emp.job_position,
                                manager = emp.manager,
                                manager_name = subgroup.name,
                                last_updated = emp.last_updated
                            }).Where(m => m.emp_code == empcode).ToList();


            return new DataTableResponse
            {
                RecordsTotal = 1,
                RecordsFiltered = 0,
                Data = employee.ToArray()
            };

            /*
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
            */
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> Post(Employee model)
        {
            _context.Employees.Add(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee updatedmodel)
        {
            /*
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.emp_code == updatedmodel.emp_code);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            */
            try
            {
                _context.Employees.Update(updatedmodel);

                await _context.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            
        }

        [Route("Manager")]
        [HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetAllManagers()
        {
            var emps = (from man in _context.Employees
                        join emp in _context.Employees on man.manager equals emp.emp_code
                        select new
                        {
                            manager = man.manager,
                            manager_name = emp.name
                        }).Distinct().ToList();
            return new DataTableResponse
            {
                RecordsTotal = 1,
                RecordsFiltered = 0,
                Data = emps.ToArray()

            };
        }




    }
        
}
