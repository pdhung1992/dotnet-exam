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
        Employee findEmp = dataContext.Employees.Find(id);
        if (findEmp == null)
        {
            return NotFound();
        }

        return View(new EmployeeModel()
        {
            id = findEmp.id,
            fullname = findEmp.fullname,
            email = findEmp.email,
            Department = findEmp.Department
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeModel updateEmp)
    {
        if (ModelState.IsValid)
        {
            dataContext.Employees.Update(new Employee()
            {
                id = updateEmp.id,
                fullname = updateEmp.fullname,
                email = updateEmp.email,
                Department = updateEmp.Department
            });
            dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(updateEmp);
    }

    public IActionResult Delete(int id)
    {
        Employee deleteEmp = dataContext.Employees.Find(id);
        if (deleteEmp == null)
        {
            return NotFound();
        }

        dataContext.Employees.Remove(deleteEmp);
        dataContext.SaveChanges();
        return RedirectToAction("Index");
    }
}