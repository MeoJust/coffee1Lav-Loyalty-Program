using System.IdentityModel.Tokens.Jwt;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class LoyaltyCard
{
    public static string keyFilePath;

    public static ServiceAccountCredential credentials;

    public static WalletobjectsService service;

    //ПОЛУЧЕНИЕ СЕРВИСНОГО АККАУНТА И ПОДКЛЮЧЕНИЕ К GOOGLE WALLET API
    public LoyaltyCard() {
        keyFilePath = "D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json";

        Auth();
    }

    public void Auth() {
        credentials = (ServiceAccountCredential)GoogleCredential
            .FromFile(keyFilePath)
            .CreateScoped(new List<string>
            {
          WalletobjectsService.ScopeConstants.WalletObjectIssuer
            })
            .UnderlyingCredential;

        service = new WalletobjectsService(
            new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });
    }

    //СОЗДАНИЕ КЛАССА КАРТЫ
    public string CreateClass(string issuerId, string classSuffix) {
        // ПРОВЕРКА СУЩЕСТВОВАНИЕ КЛАССА
        Stream responseStream = service.Loyaltyclass
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

        // СОЗДАНИЕ КЛАССА
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

        responseStream = service.Loyaltyclass
            .Insert(newClass)
            .ExecuteAsStream();

        responseReader = new StreamReader(responseStream);
        jsonResponse = JObject.Parse(responseReader.ReadToEnd());

        Console.WriteLine("Class insert response");
        Console.WriteLine(jsonResponse.ToString());

        return $"{issuerId}.{classSuffix}";
    }

    //ЧАСТИЧНОЕ ОБНОВЛЕНИЕ ИЗМЕННЕННЫХ ПОЛЕЙ КЛАССА
    public string PatchClass(string issuerId, string classSuffix) {
        // ПРОВЕРКА СУЩЕСТВОВАНИЯ КЛАССА
        Stream responseStream = service.Loyaltyclass
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
                // Something else went wrong...
                Console.WriteLine(jsonResponse.ToString());
                return $"{issuerId}.{classSuffix}";
            }
        }

        // ОБНОВЛЕНИЕ КЛАССА
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

        responseStream = service.Loyaltyclass
            .Patch(patchBody, $"{issuerId}.{classSuffix}")
            .ExecuteAsStream();

        responseReader = new StreamReader(responseStream);
        jsonResponse = JObject.Parse(responseReader.ReadToEnd());

        Console.WriteLine("Class patch response");
        Console.WriteLine(jsonResponse.ToString());

        return $"{issuerId}.{classSuffix}";
    }

    //ВСЕ ТОЖЕ САМОЕ, ТОЛЬКО ДЛЯ ОБЬЕКТА
    public string CreateObject(string issuerId, string classSuffix, string objectSuffix) {
        Stream responseStream = service.Loyaltyobject
            .Get($"{issuerId}.{objectSuffix}")
            .ExecuteAsStream();

        StreamReader responseReader = new StreamReader(responseStream);
        JObject jsonResponse = JObject.Parse(responseReader.ReadToEnd());

        if (!jsonResponse.ContainsKey("error"))
        {
            Console.WriteLine($"Object {issuerId}.{objectSuffix} already exists!");
            return $"{issuerId}.{objectSuffix}";
        }
        else if (jsonResponse["error"].Value<int>("code") != 404)
        {
            Console.WriteLine(jsonResponse.ToString());
            return $"{issuerId}.{objectSuffix}";
        }

        LoyaltyObject newObject = new LoyaltyObject
        {
            Id = $"{issuerId}.{objectSuffix}",
            ClassId = $"{issuerId}.{classSuffix}",
            State = "ACTIVE",
            HeroImage = new Image
            {
                SourceUri = new ImageUri
                {
                    Uri = "https://i.imgur.com/IQeQWbV.png"
                },
                ContentDescription = new LocalizedString
                {
                    DefaultValue = new TranslatedString
                    {
                        Language = "en-US",
                        Value = "Hero image description"
                    }
                }
            },
            TextModulesData = new List<TextModuleData>
      {
        new TextModuleData
        {
          Header = "Text module header",
          Body = "Text module body",
          Id = "TEXT_MODULE_ID"
        }
      },
            LinksModuleData = new LinksModuleData
            {
                Uris = new List<Google.Apis.Walletobjects.v1.Data.Uri>
        {
          new Google.Apis.Walletobjects.v1.Data.Uri
          {
            UriValue = "http://maps.google.com/",
            Description = "Link module URI description",
            Id = "LINK_MODULE_URI_ID"
          },
          new Google.Apis.Walletobjects.v1.Data.Uri
          {
            UriValue = "tel:6505555555",
            Description = "Link module tel description",
            Id = "LINK_MODULE_TEL_ID"
          }
        }
            },
            ImageModulesData = new List<ImageModuleData>
      {
        new ImageModuleData
        {
          MainImage = new Image
          {
            SourceUri = new ImageUri
            {
              Uri = "http://farm4.staticflickr.com/3738/12440799783_3dc3c20606_b.jpg"
            },
            ContentDescription = new LocalizedString
            {
              DefaultValue = new TranslatedString
              {
                Language = "en-US",
                Value = "Image module description"
              }
            }
          },
          Id = "IMAGE_MODULE_ID"
        }
      },
            Barcode = new Barcode
            {
                Type = "QR_CODE",
                Value = "QR code"
            },
            Locations = new List<LatLongPoint>
      {
        new LatLongPoint
        {
          Latitude = 37.424015499999996,
          Longitude = -122.09259560000001
        }
      },
            AccountId = "Account id",
            AccountName = "Account name",
            LoyaltyPoints = new LoyaltyPoints
            {
                Label = "Points",
                Balance = new LoyaltyPointsBalance
                {
                    Int__ = 800
                }
            }
        };

        responseStream = service.Loyaltyobject
            .Insert(newObject)
            .ExecuteAsStream();
        responseReader = new StreamReader(responseStream);
        jsonResponse = JObject.Parse(responseReader.ReadToEnd());

        Console.WriteLine("Object insert response");
        Console.WriteLine(jsonResponse.ToString());

        return $"{issuerId}.{objectSuffix}";
    }

    public string PatchObject(string issuerId, string objectSuffix) {
        // Check if the object exists
        Stream responseStream = service.Loyaltyobject
            .Get($"{issuerId}.{objectSuffix}")
            .ExecuteAsStream();

        StreamReader responseReader = new StreamReader(responseStream);
        JObject jsonResponse = JObject.Parse(responseReader.ReadToEnd());

        if (jsonResponse.ContainsKey("error"))
        {
            if (jsonResponse["error"].Value<int>("code") == 404)
            {
                // Object does not exist
                Console.WriteLine($"Object {issuerId}.{objectSuffix} not found!");
                return $"{issuerId}.{objectSuffix}";
            }
            else
            {
                // Something else went wrong...
                Console.WriteLine(jsonResponse.ToString());
                return $"{issuerId}.{objectSuffix}";
            }
        }

        // Object exists
        LoyaltyObject existingObject = JsonConvert.DeserializeObject<LoyaltyObject>(jsonResponse.ToString());

        // Patch the object by adding a link
        Google.Apis.Walletobjects.v1.Data.Uri newLink = new Google.Apis.Walletobjects.v1.Data.Uri
        {
            UriValue = "https://developers.google.com/wallet",
            Description = "New link description"
        };

        LoyaltyObject patchBody = new LoyaltyObject();

        patchBody.HeroImage = new Image
        {
            SourceUri = new ImageUri { Uri = "https://i.imgur.com/IQeQWbV.png" },
            ContentDescription = new LocalizedString
            {
                DefaultValue = new TranslatedString
                {
                    Language = "en-US",
                    Value = "Hero image description"
                }
            }
        };

        if (existingObject.LinksModuleData == null)
        {
            // LinksModuleData was not set on the original object
            patchBody.LinksModuleData = new LinksModuleData
            {
                Uris = new List<Google.Apis.Walletobjects.v1.Data.Uri>()
            };
        }
        else
        {
            // LinksModuleData was set on the original object
            patchBody.LinksModuleData = existingObject.LinksModuleData;
        }
        patchBody.LinksModuleData.Uris.Add(newLink);

        responseStream = service.Loyaltyobject
            .Patch(patchBody, $"{issuerId}.{objectSuffix}")
            .ExecuteAsStream();

        responseReader = new StreamReader(responseStream);
        jsonResponse = JObject.Parse(responseReader.ReadToEnd());

        Console.WriteLine("Object patch response");
        Console.WriteLine(jsonResponse.ToString());

        return $"{issuerId}.{objectSuffix}";
    }

    //СОЗДАНИЕ JWT
    public string CreateJWT(string issuerId, string classSuffix, string objectSuffix) {
        //ПРОВЕРКА НА NULL
        JsonSerializerSettings excludeNulls = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        
        //ПОЛУЧЕНИЕ КЛАССА И ОБЬЕКТА КАРТЫ
        LoyaltyClass newClass = new LoyaltyClass
        {
            Id = $"{issuerId}.{classSuffix}",
            IssuerName = "Issuer name",
            ReviewStatus = "UNDER_REVIEW",
            ProgramName = "Program name",
            ProgramLogo = new Image
            {
                SourceUri = new ImageUri
                {
                    Uri = "http://farm8.staticflickr.com/7340/11177041185_a61a7f2139_o.jpg"
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

        LoyaltyObject newObject = new LoyaltyObject
        {
            Id = $"{issuerId}.{objectSuffix}",
            ClassId = $"{issuerId}.{classSuffix}",
            State = "ACTIVE",
            HeroImage = new Image
            {
                SourceUri = new ImageUri
                {
                    Uri = "https://farm4.staticflickr.com/3723/11177041115_6e6a3b6f49_o.jpg"
                },
                ContentDescription = new LocalizedString
                {
                    DefaultValue = new TranslatedString
                    {
                        Language = "en-US",
                        Value = "Hero image description"
                    }
                }
            },
            TextModulesData = new List<TextModuleData>
      {
        new TextModuleData
        {
          Header = "Text module header",
          Body = "Text module body",
          Id = "TEXT_MODULE_ID"
        }
      },
            LinksModuleData = new LinksModuleData
            {
                Uris = new List<Google.Apis.Walletobjects.v1.Data.Uri>
        {
          new Google.Apis.Walletobjects.v1.Data.Uri
          {
            UriValue = "http://maps.google.com/",
            Description = "Link module URI description",
            Id = "LINK_MODULE_URI_ID"
          },
          new Google.Apis.Walletobjects.v1.Data.Uri
          {
            UriValue = "tel:6505555555",
            Description = "Link module tel description",
            Id = "LINK_MODULE_TEL_ID"
          }
        }
            },
            ImageModulesData = new List<ImageModuleData>
      {
        new ImageModuleData
        {
          MainImage = new Image
          {
            SourceUri = new ImageUri
            {
              Uri = "http://farm4.staticflickr.com/3738/12440799783_3dc3c20606_b.jpg"
            },
            ContentDescription = new LocalizedString
            {
              DefaultValue = new TranslatedString
              {
                Language = "en-US",
                Value = "Image module description"
              }
            }
          },
          Id = "IMAGE_MODULE_ID"
        }
      },
            Barcode = new Barcode
            {
                Type = "QR_CODE",
                Value = "QR code"
            },
            Locations = new List<LatLongPoint>
      {
        new LatLongPoint
        {
          Latitude = 37.424015499999996,
          Longitude = -122.09259560000001
        }
      },
            AccountId = "Account id",
            AccountName = "Account name",
            LoyaltyPoints = new LoyaltyPoints
            {
                Label = "Points",
                Balance = new LoyaltyPointsBalance
                {
                    Int__ = 800
                }
            }
        };

        //СОЗДАНИЕ JSON ИЗ КЛАССА И ОБЬЕКТА
        JObject serializedClass = JObject.Parse(
            JsonConvert.SerializeObject(newClass, excludeNulls));
        JObject serializedObject = JObject.Parse(
            JsonConvert.SerializeObject(newObject, excludeNulls));

        //СОЗДАНИЕ JWT В ВИДЕ JSON
        JObject jwtPayload = JObject.Parse(JsonConvert.SerializeObject(new
        {
            iss = credentials.Id,
            aud = "google",
            origins = new List<string>
      {
        "www.example.com"
      },
            typ = "savetowallet",
            payload = JObject.Parse(JsonConvert.SerializeObject(new
            {
                loyaltyClasses = new List<JObject>
        {
          serializedClass
        },
                loyaltyObjects = new List<JObject>
        {
          serializedObject
        }
            }))
        }));

        //СОЗДАНИЕ JWT В ВИДЕ СТРОКИ
        JwtPayload claims = JwtPayload.Deserialize(jwtPayload.ToString());

        RsaSecurityKey key = new RsaSecurityKey(credentials.Key);
        SigningCredentials signingCredentials = new SigningCredentials(
            key, SecurityAlgorithms.RsaSha256);
        JwtSecurityToken jwt = new JwtSecurityToken(
            new JwtHeader(signingCredentials), claims);
        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

        //ВЫВОД ПОЛУЧЕННОГО JWT-КЛЮЧА В КОНСОЛЬ
        Console.WriteLine("Add to Google Wallet link");
        Console.WriteLine($"https://pay.google.com/gp/v/save/{token}");

        return $"https://pay.google.com/gp/v/save/{token}";
    }
}