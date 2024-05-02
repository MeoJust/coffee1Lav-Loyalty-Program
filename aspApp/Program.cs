namespace aspApp
{
    //Основной класс программы
    public class Program
    {
        //Точка входа в программу
        public static void Main(string[] args) {
            //Связь с Google Wallet API
            Authorization auth = new Authorization("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            CardClass cardClass = new CardClass(auth.WalletService);
            CardObject cardObject = new CardObject(auth.WalletService);

            //Создание класса карты
            cardClass.CreateClass("3388000000022315715", "coffeOneLav");
            //Создание обьекта карты
            cardObject.CreateObject("3388000000022315715", "coffeOneLav", "1", "2");

            //Вывод списка активных карт
            LoyaltyCardService loyaltyCardService = new LoyaltyCardService(auth.WalletService);
            loyaltyCardService.GetAllLoyaltyObjects("3388000000022315715.coffeOneLav");

            LoyaltyCardManager loyaltyCardManager = new LoyaltyCardManager(auth.WalletService);
            loyaltyCardManager.UpdateLoyaltyPoints("3388000000022315715.your-issuer-id.your-class-suffix.CoffeeMan", 777);


            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.WebHost.UseUrls("http://*:5203");

            //Разрешение любых запросов из любых источников
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Подключение разрешений
            app.UseCors("AllowAll");

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
