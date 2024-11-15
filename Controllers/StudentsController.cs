using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestFinal5.Models;
using Microsoft.AspNetCore.Http;

namespace TestFinal5.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
         ViewBag.valed = HttpContext.Session.GetString("valed");
         if(ViewBag.valed !="valed")
         {
            return RedirectToAction("LogIn");
         }
         ViewBag.count = _context.Students.Count();
         if (_context.Students.Count() > 0)
         {
            ViewBag.max = _context.Students.Max(tbl => tbl.Total);
            ViewBag.min = _context.Students.Min(tbl => tbl.Total);
            ViewBag.avg = _context.Students.Average(tbl => tbl.Total);
         }
         
         return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
         ViewBag.valed = HttpContext.Session.GetString("valed");
         if (ViewBag.valed != "valed")
         {
            return RedirectToAction("LogIn");
         }
         if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
         ViewBag.valed = HttpContext.Session.GetString("valed");
         if (ViewBag.valed != "valed")
         {
            return RedirectToAction("LogIn");
         }
         return View();
        }
      [HttpGet]
      public IActionResult logIn()
      {
         return View();
      }
      public IActionResult Q1()
      {
        // var data = _context.Students.Where(tbl => tbl.Gender == "Male" && tbl.Rate == 'A').ToList();
         var data = _context.Students.OrderBy(tbl=> tbl.Total).ToList();
        
         return View(data);
      }
      public IActionResult Q2()
      {
         
         var data = _context.Students.OrderBy(tbl => tbl.Total).ToList().FirstOrDefault();
         return View(data);
      }
      [HttpPost]
      public IActionResult logIn(Student s)
      {
         
         var data = _context.Students.ToList();
         foreach (var item in data)
         {
            if(item.Passsword==s.Passsword && item.Email == s.Email)
            {
               HttpContext.Session.SetString("valed", "valed");
               return RedirectToAction("Create");
               
            }
         }
         return View();
      }

      // POST: Students/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( StudentView model)
        {

         var file = HttpContext.Request.Form.Files;
         if (file.Count > 0)
         {
            string imagePath = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
            var fileStream = new FileStream(Path.Combine("wwwroot/", "Images", imagePath), FileMode.Create);
            file[0].CopyTo(fileStream);
            model.Eimage = imagePath;
         }
         else if (model.Eimage == null && model.ID == 0)
         {
            model.Eimage = "DefaultImage.png";
         }
         else
         {
            model.Eimage = model.Eimage;
         }



         Student student=new Student();
         student.ID = model.ID;
         student.StName = model.StName;
         student.Gender = model.Gender;
         student.Total = model.FirstExame + model.SecondExame + model.FinalExame;
         if(student.Total>88)
         student.Rate = 'A';
         else if (student.Total>75)
            student.Rate = 'B';
         else if(student.Total>65)
            student.Rate = 'C';
         else if(student.Total>49)
            student.Rate = 'D';
         else
            student.Rate = 'F';
         student.Passsword = model.Passsword;
         student.Email = model.Email;
         student.Eimage = model.Eimage;
         if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
         ViewBag.valed = HttpContext.Session.GetString("valed");
         if (ViewBag.valed != "valed")
         {
            return RedirectToAction("LogIn");
         }
         if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StName,Gender,Total,Rate,Email,Passsword,Eimage")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
         ViewBag.valed = HttpContext.Session.GetString("valed");
         if (ViewBag.valed != "valed")
         {
            return RedirectToAction("LogIn");
         }
         if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
