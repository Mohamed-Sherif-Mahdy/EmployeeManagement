using EmployeeManagement_API.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DevChecksTask_UI.Controllers
{
  public class EmployeeUIController : Controller
  {
    private readonly HttpClient _client;
    public EmployeeUIController()
    {
      _client = new HttpClient();
      _client.BaseAddress = new Uri("https://localhost:44341/api/");

    }
    [HttpGet]
    public IActionResult Index()
    {
      HttpResponseMessage response = _client.GetAsync("EmployeeManagementAPI").Result;
      if (response.IsSuccessStatusCode)
      {
        var data = response.Content.ReadAsStringAsync().Result;
        var ListOfDtoEmployees = JsonConvert.DeserializeObject<List<EmployeeWithJobTitleDto>>(data);
        return View(ListOfDtoEmployees);
      }
      return Content("Bad Request");
    }

    [HttpGet]
    public IActionResult Create()
    {
      //get job titles
      HttpResponseMessage response = _client.GetAsync("JobAPI").Result;
      if (response.IsSuccessStatusCode)
      {
        var data = response.Content.ReadAsStringAsync().Result;
        var jobTitles = JsonConvert.DeserializeObject<List<string>>(data);
        ViewBag.JobTitles = jobTitles;
      }
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeWithJobTitleDto DtoEmployee)
    {
      var data = JsonConvert.SerializeObject(DtoEmployee);
      var content = new StringContent(data, Encoding.UTF8, "application/json");
      HttpResponseMessage response = _client.PostAsync("EmployeeManagementAPI", content).Result;
      if (response.IsSuccessStatusCode)
      {
        return RedirectToAction("Index");
      }
      return Content("Bad Request");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
      HttpResponseMessage response = _client.GetAsync($"EmployeeManagementAPI/{id}").Result;
      //get job titles
      HttpResponseMessage responseJobTitles = _client.GetAsync("JobAPI").Result;
      if (responseJobTitles.IsSuccessStatusCode)
      {
        var dataJobTitles = responseJobTitles.Content.ReadAsStringAsync().Result;
        var jobTitles = JsonConvert.DeserializeObject<List<string>>(dataJobTitles);
        ViewBag.JobTitles = jobTitles;
      }
      if (response.IsSuccessStatusCode)
      {
        var data = response.Content.ReadAsStringAsync().Result;
        var DtoEmployee = JsonConvert.DeserializeObject<EmployeeWithJobTitleDto>(data);
        return View(DtoEmployee);
      }
      return Content("Bad Request");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, EmployeeWithJobTitleDto DtoEmployee)
    {
      var data = JsonConvert.SerializeObject(DtoEmployee);
      var content = new StringContent(data, Encoding.UTF8, "application/json");
      HttpResponseMessage response = _client.PutAsync($"EmployeeManagementAPI/{id}", content).Result;
      if (response.IsSuccessStatusCode)
      {
        return RedirectToAction("Index");
      }
      return Content("Bad Request");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
      HttpResponseMessage response = _client.DeleteAsync($"EmployeeManagementAPI/{id}").Result;
      if (response.IsSuccessStatusCode)
      {
        return RedirectToAction("Index");
      }
      return Content("Bad Request");
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
      HttpResponseMessage response = _client.GetAsync($"EmployeeManagementAPI/{id}").Result;
      if (response.IsSuccessStatusCode)
      {
        var data = response.Content.ReadAsStringAsync().Result;
        var DtoEmployee = JsonConvert.DeserializeObject<EmployeeWithJobTitleDto>(data);
        return PartialView("_DetailsPartialView", DtoEmployee);
      }
      return Content("Bad Request");
    }

  }
}
