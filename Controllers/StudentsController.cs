using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public StudentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      IEnumerable<Student> sortedStudents = _db.Students.ToList().OrderBy(student => student.StudentName);
      return View(sortedStudents.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student, int CourseId)
    {
      _db.Students.Add(student);
      _db.SaveChanges();
      if (CourseId != 0)
      {
        _db.CourseStudent.Add(new CourseStudent() { CourseId = CourseId, StudentId = student.StudentId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

//     public ActionResult Details(int id)
//     {
//       var thisItem = _db.Items
//           .Include(item => item.JoinEntities)
//           .ThenInclude(join => join.Category)
//           .FirstOrDefault(item => item.StudentId == id);
//       return View(thisItem);
//     }

//     public ActionResult Edit(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(item => item.StudentId == id);
//       ViewBag.CourseId = new SelectList(_db.Categories, "CourseId", "Name");
//       return View(thisItem);
//     }

//     [HttpPost]
//     public ActionResult Edit(Item item, int CourseId)
//     {
//       if (CourseId != 0)
//       {
//         _db.CategoryItem.Add(new CategoryItem() { CourseId = CourseId, StudentId = item.StudentId });
//       }
//       _db.Entry(item).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult AddCategory(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(item => item.StudentId == id);
//       ViewBag.CourseId = new SelectList(_db.Categories, "CourseId", "Name");
//       return View(thisItem);
//     }

//     [HttpPost]
//     public ActionResult AddCategory(Item item, int CourseId)
//     {
//       if (CourseId != 0)
//       {
//       _db.CategoryItem.Add(new CategoryItem() { CourseId = CourseId, StudentId = item.StudentId});
//       }
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(item => item.StudentId == id);
//       return View(thisItem);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(item => item.StudentId == id);
//       _db.Items.Remove(thisItem);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     [HttpPost]
//     public ActionResult DeleteCategory(int joinId)
//     {
//       var joinEntry = _db.CategoryItem.FirstOrDefault(entry => entry.CourseStudentId == joinId);
//       _db.CategoryItem.Remove(joinEntry);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
  }
}