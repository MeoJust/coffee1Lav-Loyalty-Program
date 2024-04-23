using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace aspApp.Controllers
{
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index([FromForm] RegistrationData data) {
            string json = JsonConvert.SerializeObject(data);
            string path = "D:\\_art\\_csharp\\coffeOneLoveProj\\_mySuperMegaDataBase";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = Path.Combine(path, $"{data.Name}-{data.Phone}.json");
            System.IO.File.WriteAllText(filename, json);
            return Ok("Registration successful");
        }
    }

    public class RegistrationData
    {
        public string Name { get; set; } = "Chelic";
        public string Phone { get; set; } = "+37525555555";
    }
}
