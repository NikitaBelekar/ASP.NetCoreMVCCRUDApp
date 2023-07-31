using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        //Existing WebAPI hosted on server
        string Baseurl = "https://getinvoices.azurewebsites.net/";

        public async Task<ActionResult> Index()
        {
            List<Customer> CustInfo = new List<Customer>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Data format in which request is sent
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //sending request to find WebAPI service resource using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Customers");

                //Check response success

                if (Res.IsSuccessStatusCode)
                {
                    //Store response in a variable
                    var CustResponse = Res.Content.ReadAsStringAsync().Result;

                    //deserialize reponse rescieved from WebAPI
                    CustInfo = JsonConvert.DeserializeObject<List<Customer>>(CustResponse);

                }

            }
            //Return the customer list to view
            return View(CustInfo);

            }

        public async Task<ActionResult> Details(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            Customer custobj = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var result = await client.GetAsync($"api/Customer/{id}");

                if (result.IsSuccessStatusCode)
                {
                    custobj = await result.Content.ReadAsAsync<Customer>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error.");
                }
            }

           
            return View(custobj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer custobj)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var response = await client.PostAsJsonAsync("api/Customer/", custobj);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error.");
                    }
                }
            }
            return View(custobj);
        }

        public async Task<ActionResult> Edit(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Customer custobj = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var result = await client.GetAsync($"api/Customer/{id}");

                if (result.IsSuccessStatusCode)
                {
                    custobj = await result.Content.ReadAsAsync<Customer>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error.");
                }
            }
           
            return View(custobj);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer custobj)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var response = await client.PutAsJsonAsync("api/Customer", custobj);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error.");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(custobj);
        }

        public async Task<ActionResult> Delete(string id)
        {
           
            Customer custobj = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var result = await client.GetAsync($"api/Customer/{id}");

                if (result.IsSuccessStatusCode)
                {
                    custobj = await result.Content.ReadAsAsync<Customer>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error.");
                }
            }

         
            return View(custobj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var response = await client.DeleteAsync($"api/Customer/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View();
        }

    }


}
