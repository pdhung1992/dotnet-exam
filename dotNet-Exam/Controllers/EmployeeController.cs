using dotNet_Exam.Entities;
using dotNet_Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNet_Exam.Controllers;

public class EmployeeController: Controller
{
    private readonly DataContext dataContext;

    public EmployeeController(DataContext context)
    {
        dataContext = context;
    }

    public IActionResult Index()
    {
        List<Employee> empList = dataContext.Employees.Include(emp => emp.Department).ToList();
        return View(empList);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EmployeeModel newEmp)
    {
        
        if (ModelState.IsValid)
        {
            Department addDepartment = dataContext.Departments.Find(newEmp.depId);
            if (addDepartment == null)
            {
                return BadRequest(new { Message = "Invalid Deapartment Id. Department not found." });
            }
            
            dataContext.Employees.Add(new Employee
            {
                fullname = newEmp.fullname,
                email = newEmp.email,
                Department = addDepartment
                
            });
            dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(newEmp);
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