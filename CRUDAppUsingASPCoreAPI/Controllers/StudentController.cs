using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CRUDAppUsingASPCoreAPI.Controllers
{
    public class StudentController : Controller
    {
        private readonly string url;
        private static readonly HttpClient client = new HttpClient();

        public StudentController(IConfiguration configuration)
        {
            url = configuration["ApiSettings:BaseUrl"] ?? throw new ArgumentNullException("ApiSettings:BaseUrl is missing");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                students = data ?? new List<Student>();
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student added successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Failed to create student: " + error);
                return View(std);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = await client.GetAsync(url + id);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(url + std.id, content);
            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Failed to update student: " + error);
                return View(std);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = await client.GetAsync(url + id);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(url + id);
            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student deleted successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}