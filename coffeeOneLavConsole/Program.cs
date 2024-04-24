//////////////////////////////////////////////////////////////////
//!СОЗДАЕТ СТАТИЧНУЮ КАРТУ, В ФИНАЛЬНОМ ПРОЕКТЕ НЕ ИСПОЛЬЗУЕТСЯ!//
//////////////////////////////////////////////////////////////////

namespace coffeeOneLavConsole
{
    internal class Program
    {
        static void Main(string[] args) {
            LoyaltyCard card = new LoyaltyCard();

            card.CreateClass("3388000000022315715", "coffeOneLav");
            card.PatchClass("3388000000022315715", "coffeOneLav");
            card.CreateObject("3388000000022315715", "coffeOneLav", "coffeOneLav01");
            card.PatchObject("3388000000022315715", "coffeOneLav01");
            card.CreateJWT("3388000000022315715", "coffeOneLav", "coffeOneLav01");
        }
    }
}
