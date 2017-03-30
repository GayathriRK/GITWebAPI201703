using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        //Display of all Contacts
        public IEnumerable<Employee> GetEmployees()
        {
            using(Test_ProjectEntities Entities=new Test_ProjectEntities())
            {
                return Entities.Employees.ToList();
            }
        }
        //Display the contact based on ID
        public Employee GetEmployeebyId(int id)
        {
            using (Test_ProjectEntities Entities = new Test_ProjectEntities())
            {
                return Entities.Employees.FirstOrDefault(e => e.EMP_ID == id);
            }
        }
        //Add Contacts
        public HttpResponseMessage PostEmployee([FromBody] Employee emp)
        {
            try
            {
                using (Test_ProjectEntities Entities = new Test_ProjectEntities())
                {                    
                    Entities.Employees.Add(emp);
                    Entities.SaveChanges();
                    var result = Request.CreateResponse(HttpStatusCode.Created, emp);
                    return result;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Edit Contact
        public HttpResponseMessage PutEmployee(int id,[FromBody] Employee emp)
        {
            try
            {
                using (Test_ProjectEntities Entities = new Test_ProjectEntities())
                {
                    var entity = Entities.Employees.FirstOrDefault(e => e.EMP_ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Not Found");
                    }
                    else
                    {
                        entity.FIRST_NAME = emp.FIRST_NAME;
                        entity.LAST_NAME = emp.LAST_NAME;
                        entity.EMAIL = emp.EMAIL;
                        entity.PHONE_NUMBER = emp.PHONE_NUMBER;
                        entity.EMP_STATUS = emp.EMP_STATUS;                        
                        Entities.SaveChanges();
                        var result = Request.CreateResponse(HttpStatusCode.OK, emp);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Delete Contact
        public HttpResponseMessage DeleteEmployee(int id)
        {
            try
            {
                using (Test_ProjectEntities Entities = new Test_ProjectEntities())
                {
                    var entity = Entities.Employees.FirstOrDefault(e => e.EMP_ID == id);
                    if(entity==null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Not Found");
                    }
                    else
                    {
                        Entities.Employees.Remove(entity);
                        Entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
