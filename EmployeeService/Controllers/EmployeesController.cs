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
        public HttpResponseMessage GetEmployees()
        {
            using(Test_ProjectEntities Entities=new Test_ProjectEntities())
            {                
               var entity=Entities.Employees.ToList();
               if(entity==null)
               {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employees Not Found");
               }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }
        //Display the contact based on ID
        public HttpResponseMessage GetEmployeebyId(int id)
        {
            using (Test_ProjectEntities Entities = new Test_ProjectEntities())
            {                
                var entity= Entities.Employees.FirstOrDefault(e => e.EMP_ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Not Found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }
        //Add Contacts
        public HttpResponseMessage PostEmployee([FromBody] Employee emp)
        {
            try
            {
                using (Test_ProjectEntities Entities = new Test_ProjectEntities())
                {
                    //var First_name = Entities.Employees.FirstOrDefault(c=>c.FIRST_NAME==emp.FIRST_NAME);
                    var Email = Entities.Employees.FirstOrDefault(c=>c.EMAIL==emp.EMAIL);
                    if (string.IsNullOrEmpty(emp.FIRST_NAME) || string.IsNullOrWhiteSpace(emp.FIRST_NAME))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Firstname is Required");
                    }
                    else if (emp.FIRST_NAME.ToLower().Trim() == emp.LAST_NAME.ToLower().Trim())
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Firstname and Lastname can not be same");
                    }
                    else if (Email != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "The Employee email already exists");
                    }
                    else if (string.IsNullOrEmpty(emp.EMAIL) || string.IsNullOrWhiteSpace(emp.EMAIL))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Email is Required");
                    }
                    else if (string.IsNullOrEmpty(emp.PHONE_NUMBER) || string.IsNullOrWhiteSpace(emp.PHONE_NUMBER))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Phone Number is Required");
                    }
                    else if (string.IsNullOrEmpty(emp.EMP_STATUS))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Please enter Employee Status");
                    }
                    else
                        {
                            if (emp.EMP_STATUS.ToLower() == "active" || emp.EMP_STATUS.ToLower() == "inactive")
                            {
                                Entities.Employees.Add(emp);
                                Entities.SaveChanges();
                                var result = Request.CreateResponse(HttpStatusCode.Created, emp);
                                return result;
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Employee Status should be Active or Inactive");

                            }
                        }
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
                        entity.EMAIL = emp.EMAIL.Trim();
                        entity.PHONE_NUMBER = emp.PHONE_NUMBER.Trim();
                        entity.EMP_STATUS = emp.EMP_STATUS;
                        if (string.IsNullOrEmpty(emp.FIRST_NAME) || string.IsNullOrWhiteSpace(emp.FIRST_NAME))
                        {
                           return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Firstname is Required");
                        }                        
                        else if (emp.FIRST_NAME.ToLower().Trim() == emp.LAST_NAME.ToLower().Trim())
                        {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable,"Firstname and Lastname can not be same");
                        }
                        else if (string.IsNullOrEmpty(emp.EMAIL) || string.IsNullOrWhiteSpace(emp.EMAIL))
                        {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Email is Required");
                        }
                        else if (string.IsNullOrEmpty(emp.PHONE_NUMBER) || string.IsNullOrWhiteSpace(emp.PHONE_NUMBER))
                        {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Phone Number is Required");
                        }
                        else if (string.IsNullOrEmpty(emp.EMP_STATUS))
                        {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Please enter Employee Status");
                        }
                        
                        else
                        {
                            if (emp.EMP_STATUS.ToLower() == "active" || emp.EMP_STATUS.ToLower() == "inactive")
                            {
                                Entities.SaveChanges();
                                var result = Request.CreateResponse(HttpStatusCode.OK, emp);
                                return result;
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Employee Status should be Active or Inactive");
                            
                            }
                        }
                        
                        
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
                        return Request.CreateResponse(HttpStatusCode.OK,"Employee Deleted Successfully");
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
