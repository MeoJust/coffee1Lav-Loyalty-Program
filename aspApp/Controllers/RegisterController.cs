using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace aspApp.Controllers
{
    //УКАЗАНИЕ ПУТИ ДЛЯ ЗАПРОСА
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        //POST ЗАПРОС БЕЗ АВТОРИЗАЦИИ
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index([FromForm] RegistrationData data) {
            //СОХРАНЕНИЕ ДАННЫХ В JSON ФОРМАТЕ
            string json = JsonConvert.SerializeObject(data);
            //ТУТ УКАЗЫВАЕШЬ ПУТЬ ДЛЯ СОХРАНЕНИЯ
            string path = "D:\\_art\\_csharp\\coffeOneLoveProj\\_mySuperMegaDataBase";
            //ЕСЛИ ПАПКА НЕ СУЩЕСТВУЕТ, ОНА БУДЕТ СОЗДАНА
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //КОНСТРУКТОР СОЗДАНИЯ ИМЕНИ ФАЙЛА
            string filename = Path.Combine(path, $"{data.Name}-{data.Phone}.json");
            System.IO.File.WriteAllText(filename, json);

            // После сохранения данных регистрации, создаем карту лояльности
            LoyaltyCard card = new LoyaltyCard();
            //ЦЫФРЫ - ISSUER ID ИЗ КОНСОЛИ GOOGLE WALLET, ДАЛЬШЕ НАЗВАНИЕ КЛАССА КАРТЫ(МОЖНО, В ПРИНЦИПЕ, ЛЮБОЕ)
            string classId = "3388000000022315715.coffeOneLav";
            string objectId = $"{classId}.{data.Name}";

            // СОЗДАНИЕ ОБЬЕКТ КАРТЫ С ИМЕНЕМ ПОЛЬЗОВАТЕЛЯ
            string createdObjectId = card.CreateObject("3388000000022315715", "coffeOneLav", objectId, data.Name);

            // СОЗДАНИЕ ССЫЛКИ С JWT КЛЮЧОМ ДЛЯ КАРТЫ ПОЛЬЗОВАТЕЛЯ
            string jwtLink = card.CreateJWT("3388000000022315715", "coffeOneLav", objectId);

            // ВЫЗОВ ССЫЛКИ НА КАРТУ
            return Ok(new { message = "Registration successful", jwtLink });
        }
    }
    //ОЖИДАЕМЫЕ ДАННЫЕ ПОЛЬЗОВАТЕЛЯ
    public class RegistrationData
    {
        public string Name { get; set; } = "Chelic";
        public string Phone { get; set; } = "+37525555555";
    }
}
