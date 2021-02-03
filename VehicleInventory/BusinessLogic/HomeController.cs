using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using VehicleInventory.Models;

namespace VehicleInventory.UI
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Vehicle> list = null;
            using (var client = new HttpClient())
            {
                var baseUrl = Request.Host;
                client.BaseAddress = new Uri($"{Request.Scheme}://{baseUrl.Host}:{baseUrl.Port}/api/");
                var responseTask = client.GetAsync("vehicle");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Vehicle>>();
                    readTask.Wait();

                    list = readTask.Result;
                }
                else
                {
                    list = Enumerable.Empty<Vehicle>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vehicle vehicle)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = Request.Host;
                client.BaseAddress = new Uri($"{Request.Scheme}://{baseUrl.Host}:{baseUrl.Port}/api/vehicle");

                var postTask = client.PostAsJsonAsync("vehicle", vehicle);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(vehicle);
        }

        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = Request.Host;
                client.BaseAddress = new Uri($"{Request.Scheme}://{baseUrl.Host}:{baseUrl.Port}/api/");

                var deleteTask = client.DeleteAsync("vehicle/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Vehicle vehicle = null;

            using (var client = new HttpClient())
            {
                var baseUrl = Request.Host;
                client.BaseAddress = new Uri($"{Request.Scheme}://{baseUrl.Host}:{baseUrl.Port}/api/");

                var responseTask = client.GetAsync("vehicle/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Vehicle>();
                    readTask.Wait();

                    vehicle = readTask.Result;
                }
            }

            return View(vehicle);
        }

        [HttpPost]
        public ActionResult Edit(Vehicle vehicle)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = Request.Host;
                client.BaseAddress = new Uri($"{Request.Scheme}://{baseUrl.Host}:{baseUrl.Port}/api/vehicle");

                var putTask = client.PutAsJsonAsync("vehicle", vehicle);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(vehicle);
        }
    }
}