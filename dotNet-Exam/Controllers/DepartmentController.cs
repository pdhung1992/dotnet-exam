using dotNet_Exam.Entities;
using dotNet_Exam.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_Exam.Controllers;

public class DepartmentController: Controller
{
    private readonly DataContext dataContext;

    public DepartmentController(DataContext context)
    {
        dataContext = context;
    }

    public IActionResult Index()
    {
        List<Department> depList = dataContext.Departments.ToList();
        return View(depList);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(DepartmentModel newDepartment)
    {
        if (ModelState.IsValid)
        {
            dataContext.Departments.Add(new Department { name = newDepartment.name });
            dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(newDepartment);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Department findDep = dataContext.Departments.Find(id);
        if (findDep == null)
        {
            return NotFound();
        }

        return View(new DepartmentModel()
        {
            id = findDep.id,
            name = findDep.name
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(DepartmentModel updateDep)
    {
        if (ModelState.IsValid)
        {
            dataContext.Departments.Update(new Department
            {
                id = updateDep.id,
                name = updateDep.name
            });
            dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(updateDep);
    }

    public IActionResult Delete(int id)
    {
        Department deleteDep = dataContext.Departments.Find(id);
        if (deleteDep == null)
        {
            return NotFound();
        }

        dataContext.Departments.Remove(deleteDep);
        dataContext.SaveChanges();
        return RedirectToAction("Index");
    }
}