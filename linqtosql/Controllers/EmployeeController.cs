using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace linqtosql.Models
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeclassDataContext db = new EmployeeclassDataContext();
        public ActionResult Index()
        {
            IList<Employee_Class> employelist = new List<Employee_Class>();
            var query = from qr in db.Employees select qr;
            var listdata = query.ToList();

            foreach (var employedata in listdata)
            {
                employelist.Add(new Employee_Class()
                {
                    Id = employedata.Id,
                    Name = employedata.Name,
                    PhoneNumber=employedata.PhoneNumber,
                    Address = employedata.Address,
                    Email = employedata.Email,
                });
            }
            return View(employelist);
        }

        public ActionResult Create()
        {
            Employee_Class emps = new Employee_Class();
            return View(emps);
        }

        [HttpPost]
        public ActionResult Create(Employee_Class mod)
        {
            Employee emps = new Employee();
            emps.Name = mod.Name;
            emps.PhoneNumber = mod.PhoneNumber;
            emps.Address = mod.Address;
            emps.Email = mod.Email;
            db.Employees.InsertOnSubmit(emps);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee_Class model = db.Employees.Where(val => val.Id == id).Select(val => new Employee_Class()
            {
                Id = val.Id,
                Name = val.Name,
                Address = val.Address,
                Email = val.Email,
                PhoneNumber=val.PhoneNumber
            }).SingleOrDefault();

            return View(model);

        }

        public ActionResult Edit(Employee_Class mod)
        {
            Employee emp = db.Employees.Where(val => val.Id == mod.Id).Single<Employee>();
            emp.Id = mod.Id;
            emp.Name = mod.Name;
            emp.Address = mod.Address;
            emp.PhoneNumber = mod.PhoneNumber;
            emp.Email = mod.Email;
            db.SubmitChanges();
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            Employee_Class emp = db.Employees.Where(val => val.Id == id).Select(val => new Employee_Class()
            {
                Id = val.Id,
                Name = val.Name,
                Address = val.Address,
                PhoneNumber=val.PhoneNumber,
                Email = val.Email
            }).SingleOrDefault();

            return View(emp);
        }

        [HttpPost]
        public ActionResult Delete(Employee_Class mod)
        {
            Employee emp = db.Employees.Where(val => val.Id == mod.Id).Single<Employee>();
            db.Employees.DeleteOnSubmit(emp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Employee_Class emp = db.Employees.Where(val => val.Id == id).Select(val => new Employee_Class()
            {
                Id = val.Id,
                Name = val.Name,
                Address = val.Address,
                PhoneNumber = val.PhoneNumber,
                Email = val.Email
            }).SingleOrDefault();
            return View(emp);
        }
    }
}