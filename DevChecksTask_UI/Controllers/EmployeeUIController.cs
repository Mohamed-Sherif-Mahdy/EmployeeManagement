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
      _client.BaseAddress = new Uri("http://localhost:5046/api/");

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
      HttpResponseMessage response = _client.GetAsync("EmployeeManagementAPI/JobTitles").Result;
      if (response.IsSuccessStatusCode)
      {
        var data = response.Content.ReadAsStringAsync().Result;
        var jobTitles = JsonConvert.DeserializeObject<List<string>>(data);
        ViewBag.JobTitles = jobTitles;
      }
      return View();
    }
    [HttpPost]
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
  }
}
