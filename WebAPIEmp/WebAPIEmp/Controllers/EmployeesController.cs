using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIEmp.Models;

namespace WebAPIEmp.Controllers
{
    public class EmployeesController : ApiController
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee{ Id=1,Name="Avnish",JoiningTime="1/17/21",Age=24 },
            new Employee{ Id=2,Name="manish",JoiningTime="1/15/21",Age=23 }
        };


        public IEnumerable<Employee > getAllEmployee()
        {
            return employees;
        }

        public IHttpActionResult getEmployee(int id)
        {
            var emp = employees.FirstOrDefault(a => a.Id == id);
            if(emp==null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        [HttpPost]
        public HttpResponseMessage  AddEmployee([FromBody ]Employee emps)
        {
            try
            {
                employees.Add(emps);
                var returMessage = Request.CreateResponse(HttpStatusCode.Created, employees);
                return returMessage;
            }
            catch(Exception ex)
            {
               return  Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
            
        }
        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            try
            {
                var empDel = employees.FirstOrDefault(a => a.Id == id);
                if (empDel == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with id = " + id.ToString() + " not found");
                }
                else
                {
                    employees.RemoveAt(id);
                    return Request.CreateResponse(HttpStatusCode.OK, "Employee with Id = " + id.ToString() + " is deleted");
                }
            }

            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
           
        }

        [HttpPut ]
        public HttpResponseMessage UpdateEmployee(int id,[FromBody ]Employee employee )
        {
            try
            {
                var empExist = employees.FirstOrDefault(e => e.Id == id);
                if (empExist == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with id = " + id.ToString() + " not found");
                }
                else
                {
                    empExist.Id = employee.Id;
                    empExist.Name = employee.Name;
                    empExist.Age = employee.Age;
                    empExist.JoiningTime = employee.JoiningTime;
                    return Request.CreateResponse(HttpStatusCode.OK,"Employee with Id = " + id.ToString() + " is updated");
                }

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

         
    }
}
