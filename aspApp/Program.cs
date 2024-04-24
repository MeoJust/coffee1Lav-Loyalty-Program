namespace aspApp
{
    public class Program
    {
        public static void Main(string[] args) {
            //ССЫЛКА НА LOYALTY CARD КЛАСС
            LoyaltyCard card = new LoyaltyCard();

            //ПРИ ПЕРВОМ ЗАПУСКЕ, ДОЛЖЕН СОЗДАТЬСЯ КЛАСС КАРТЫ. ПРОВЕРЬ В КОНСОЛИ
            //ЦЫФРЫ - ISSUER ID ИЗ КОНСОЛИ GOOGLE WALLET, ДАЛЬШЕ НАЗВАНИЕ КЛАССА КАРТЫ(МОЖНО, В ПРИНЦИПЕ, ЛЮБОЕ)
            card.CreateClass("3388000000022315715", "coffeOneLav");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.WebHost.UseUrls("http://*:5203");

            //РАЗРЕШИТЬ ЛЮБЫЕ ЗАПРОСЫ ИЗ ЛЮБЫХ ИСТОЧНИКОВ
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

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //ПОДКЛЮЧЕНИЕ РАЗРЕШЕНИЙ
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
