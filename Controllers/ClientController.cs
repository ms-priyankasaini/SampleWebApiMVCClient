using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using SampleWebApiClient.Models;

namespace SampleWebApiClient.Controllers
{
    public class ClientController : Controller
    {
        string base_url = "http://localhost:59070/api/";


        //Get All Employee List
        public ActionResult EmployeeList()
        {
            IEnumerable<Employee> empList = null;

            try {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(base_url);

                        //Called Employee default GET All records  
                        //GetAsync to send a GET request   
                        var responseTask = client.GetAsync("Employee");
                        responseTask.Wait();

                        //To store result of web api response.   
                        var result = responseTask.Result;

                        //If success received   
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                            readTask.Wait();
                            empList = readTask.Result;

                        }
                        else
                        {
                            ViewBag.Status = result.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
                catch(Exception ex)
                {
                    ViewBag.Status = "An Exception occured - "+ex.Message;
                }

                return View(empList);
        }


        //Get Employee Details to Disply for Edit
        public ActionResult Edit(int id)
        {
            Employee emp = null;
            
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(base_url);
                    //HTTP GET
                    var responseTask = client.GetAsync("Employee?id=" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Employee>();
                        readTask.Wait();

                        emp = readTask.Result;
                    }
                    else
                    {
                        ViewBag.Status = result.Content.ReadAsStringAsync().Result;
                    }

                }
            }
            catch(Exception ex)
            {
                ViewBag.Status = "An Exception occured - "+ex.Message;
            }
            
            return View(emp);
        }


        //Edit Employee Details, posting to Web Api
        [HttpPost]
        public ActionResult Edit(Employee emp_modify)
        {
            try { 
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(base_url);

                        //HTTP POST
                        var putTask = client.PutAsJsonAsync<Employee>("Employee", emp_modify);
                        putTask.Wait();

                        var result = putTask.Result;


                        if (result.IsSuccessStatusCode)
                        {
                           TempData["Status"]  = result.Content.ReadAsStringAsync().Result.Replace('"', ' ') + " (Emp Name:" + emp_modify.first_name + ", Emp Id: " + emp_modify.id + ")";
                            return RedirectToAction("EmployeeList");
                        }
                        else
                        {
                            ViewBag.Status = result.Content.ReadAsStringAsync().Result;
                        }
                    }
            }
            catch (Exception ex)
            {
                ViewBag.Status = "An Exception occured - " + ex.Message;
            }

            return View(emp_modify);
        }

        //Return Create view to Create New Employee
        public ActionResult Create()
        {
            return View(new Employee());
        }

        //The Post method to Create Employee, posting to Web Api
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(base_url);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Employee>("Employee", emp);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Status"] = "Employee Created Successfully, " + result.Content.ReadAsStringAsync().Result.Replace('"', ' ');
                        return RedirectToAction("EmployeeList");
                    }
                    else
                    {
                        ViewBag.Status = result.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Status = "An Exception occured - " + ex.Message;
            }
            return View(emp);
        }

        public ActionResult Details(int id)
        {
            Employee emp = null;

            try { 
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(base_url);
                       //HTTP GET
                        var responseTask = client.GetAsync("Employee?id=" + id.ToString());
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<Employee>();
                            readTask.Wait();
                            emp = readTask.Result;
                        }
                        else
                        {
                            ViewBag.Status = result.Content.ReadAsStringAsync().Result;
                        }
                    }
            }
            catch (Exception ex)
            {
                ViewBag.Status = "An Exception occured - " + ex.Message;
            }

            return View(emp);
        }

        //To Delete Employee Record, Calling Web Api
        public ActionResult Delete(int id)
        {
            try{
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(base_url);

                        //HTTP DELETE
                        var deleteTask = client.DeleteAsync("Employee/id?=" + id.ToString());
                        deleteTask.Wait();

                        var result = deleteTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            string str = "Employee Deleted Successfully, Emp Id :"+ id.ToString();
                            TempData["Status"] = str;
                            return RedirectToAction("EmployeeList");
                        }
                        else
                        {
                            ViewBag.Status = result.Content.ReadAsStringAsync().Result;
                        }
                    }
            }
            catch (Exception ex)
            {
                ViewBag.Status = "An Exception occured - " + ex.Message;
            }

            return RedirectToAction("EmployeeList");
        }
 
    }
}