using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace aspApp.Controllers
{
    //Контроллер для регистрации пользователей
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        //POST запрос без авторизации
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index([FromForm] User user) {
            //сщхранение данных в json формате
            string json = JsonConvert.SerializeObject(user);
            //путь для сохранения данных
            string path = "D:\\_art\\_csharp\\coffeOneLoveProj\\_mySuperMegaDataBase";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = Path.Combine(path, $"{user.Name}-{user.Phone}.json");
            System.IO.File.WriteAllText(filename, json);

            // После сохранения данных регистрации, создаем карту лояльности
            Authorization auth = new Authorization("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            CardObject cardObject = new CardObject(auth.WalletService);
            JWTGen jwtGen = new JWTGen(auth.Credentials);

            string classId = "3388000000022315715.coffeOneLav";

            string objectId = $"{user.Name}";


            // Создание обьекта карты с данными пользователя
            string createdObjectId = cardObject.CreateObject("3388000000022315715", "coffeOneLav", objectId, user.Name);

            // Создание JWT ссылки на карту
            string jwtLink = jwtGen.CreateJWT("3388000000022315715", "coffeOneLav", objectId);

            // Вызов ссылки на карту
            return Ok(new { message = "Registration successful", jwtLink });
        }
    }
}
