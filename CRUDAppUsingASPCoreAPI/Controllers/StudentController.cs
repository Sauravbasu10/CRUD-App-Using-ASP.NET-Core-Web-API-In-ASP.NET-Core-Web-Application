using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CRUDAppUsingASPCoreAPI.Controllers
{
    public class StudentController : Controller
    {
        private readonly string url = "https://localhost:7178/api/StudentAPI/";
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                if (data != null)
                {
                    students = data;
                }
            }

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student added successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                string error = response.Content.ReadAsStringAsync().Result;
                ModelState.AddModelError("", "Failed to create student: " + error);
                return View(std);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if(response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
           
            }
            return View(std);
        }


        [HttpPost]
        public IActionResult Edit(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(url + std.id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                string error = response.Content.ReadAsStringAsync().Result;
                ModelState.AddModelError("", "Failed to update student: " + error);
                return View(std);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }

            }
            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
           
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student deleted successfully.";
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
                 