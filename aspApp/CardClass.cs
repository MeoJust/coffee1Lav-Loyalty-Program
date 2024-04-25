﻿using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;
using Newtonsoft.Json.Linq;

namespace aspApp
{
    //Создание класса карты
    public class CardClass
    {
        //Подключение к Google Wallet API
        private readonly WalletobjectsService _service;

        public CardClass(WalletobjectsService service) {
            _service = service;
        }

        public string CreateClass(string issuerId, string classSuffix) {
            // ПРОВЕРКА СУЩЕСТВОВАНИЕ КЛАССА
            Stream responseStream = _service.Loyaltyclass
                .Get($"{issuerId}.{classSuffix}")
                .ExecuteAsStream();

            StreamReader responseReader = new StreamReader(responseStream);
            JObject jsonResponse = JObject.Parse(responseReader.ReadToEnd());

            if (!jsonResponse.ContainsKey("error"))
            {
                Console.WriteLine($"Class {issuerId}.{classSuffix} already exists!");
                return $"{issuerId}.{classSuffix}";
            }
            else if (jsonResponse["error"].Value<int>("code") != 404)
            {
                Console.WriteLine(jsonResponse.ToString());
                return $"{issuerId}.{classSuffix}";
            }

            // Создание нового класса карты
            LoyaltyClass newClass = new LoyaltyClass
            {
                Id = $"{issuerId}.{classSuffix}",
                IssuerName = "coffeeOneLav",
                ReviewStatus = "UNDER_REVIEW",
                ProgramName = "CoffeeOneLav Love Story",
                ProgramLogo = new Image
                {
                    SourceUri = new ImageUri
                    {
                        Uri = "https://i.imgur.com/Gzj9MZO.png"
                    },
                    ContentDescription = new LocalizedString
                    {
                        DefaultValue = new TranslatedString
                        {
                            Language = "en-US",
                            Value = "Logo description"
                        }
                    }
                }
            };

            responseStream = _service.Loyaltyclass
                .Insert(newClass)
                .ExecuteAsStream();

            responseReader = new StreamReader(responseStream);
            jsonResponse = JObject.Parse(responseReader.ReadToEnd());

            Console.WriteLine("Class insert response");
            Console.WriteLine(jsonResponse.ToString());

            return $"{issuerId}.{classSuffix}";
        }

        //Обновление класса
        public string PatchClass(string issuerId, string classSuffix) {
            // Проверка существования класса
            Stream responseStream = _service.Loyaltyclass
                .Get($"{issuerId}.{classSuffix}")
                .ExecuteAsStream();

            StreamReader responseReader = new StreamReader(responseStream);
            JObject jsonResponse = JObject.Parse(responseReader.ReadToEnd());

            if (jsonResponse.ContainsKey("error"))
            {
                if (jsonResponse["error"].Value<int>("code") == 404)
                {
                    Console.WriteLine($"Class {issuerId}.{classSuffix} not found!");
                    return $"{issuerId}.{classSuffix}";
                }
                else
                {
                    Console.WriteLine(jsonResponse.ToString());
                    return $"{issuerId}.{classSuffix}";
                }
            }

            LoyaltyClass patchBody = new LoyaltyClass
            {
                IssuerName = "coffeeOneLav",
                ProgramName = "CoffeeOneLav Love Story",
                ProgramLogo = new Image
                {
                    SourceUri = new ImageUri
                    {
                        Uri = "https://i.imgur.com/Gzj9MZO.png"
                    },
                    ContentDescription = new LocalizedString
                    {
                        DefaultValue = new TranslatedString
                        {
                            Language = "en-US",
                            Value = "Logo description"
                        }
                    }
                },

                HomepageUri = new Google.Apis.Walletobjects.v1.Data.Uri
                {
                    UriValue = "https://developers.google.com/wallet",
                    Description = "Homepage description"
                },

                ReviewStatus = "UNDER_REVIEW"
            };

            responseStream = _service.Loyaltyclass
                .Patch(patchBody, $"{issuerId}.{classSuffix}")
                .ExecuteAsStream();

            responseReader = new StreamReader(responseStream);
            jsonResponse = JObject.Parse(responseReader.ReadToEnd());

            Console.WriteLine("Class patch response");
            Console.WriteLine(jsonResponse.ToString());

            return $"{issuerId}.{classSuffix}";
        }

    }
}
