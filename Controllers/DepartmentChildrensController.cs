using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanksCardAPI.Models;

namespace ThanksCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentChildrensController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DepartmentChildrensController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/DepartmentChildrens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentChildren>>> GetDepartmentChildrens()
        {
            // Include を指定することで Department (Department) を同時に取得する。
            return await _context.DepartmentChildrens
                                    .Include(DepartmentChildren => DepartmentChildren.Department)
                                    .ToListAsync();
            //return await _context.Users.ToListAsync();
        }

        // GET: api/DepartmentChildrens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentChildren>> GetDepartmentChildren(long id)
        {
            var departmentChildren = await _context.DepartmentChildrens.FindAsync(id);

            if (departmentChildren == null)
            {
                return NotFound();
            }

            return departmentChildren;
        }

        // PUT: api/DepartmentChildrens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentChildren(long id, DepartmentChildren departmentChildren)
        {
            if (id != departmentChildren.Id)
            {
                return BadRequest();
            }

            // Department には既に存在しているユーザが入るため、更新の対象から外す。
            //_context.Departments.Attach(departmentChildren.Department);

            _context.Entry(departmentChildren).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentChildrenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DepartmentChildrens
        [HttpPost]
        public async Task<ActionResult<User>> PostDepartmentChildren(DepartmentChildren departmentChildren)
        {
            // Department には既に存在しているユーザが入るため、更新の対象から外す。
            //_context.Departments.Attach(user.Department);

            _context.DepartmentChildrens.Add(departmentChildren);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentChildren", new { id = departmentChildren.Id }, departmentChildren);
        }

        // DELETE: api/DepartmentChildrens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentChildren>> DeleteDepartmentChildren(long id)
        {
            var departmentChildren = await _context.DepartmentChildrens.FindAsync(id);
            if (departmentChildren == null)
            {
                return NotFound();
            }

            _context.DepartmentChildrens.Remove(departmentChildren);
            await _context.SaveChangesAsync();

            return departmentChildren;
        }

        private bool DepartmentChildrenExists(long id)
        {
            return _context.DepartmentChildrens.Any(e => e.Id == id);
        }
    }
}
