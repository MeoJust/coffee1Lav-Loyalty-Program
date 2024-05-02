using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace aspApp
{
    //Создание объекта карты
    public class CardObject
    {
        //Подключение к Google Wallet API
        private readonly WalletobjectsService _service;

        public CardObject(WalletobjectsService service) {
            _service = service;
        }

        //Создание объекта
        public string CreateObject(string issuerId, string classSuffix, string objectSuffix, string userName) {
            Stream responseStream = _service.Loyaltyobject
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
                AccountName = userName,
                LoyaltyPoints = new LoyaltyPoints
                {
                    Label = "Points",
                    Balance = new LoyaltyPointsBalance
                    {
                        Int__ = 777
                    }
                }
            };

            responseStream = _service.Loyaltyobject
                .Insert(newObject)
                .ExecuteAsStream();
            responseReader = new StreamReader(responseStream);
            jsonResponse = JObject.Parse(responseReader.ReadToEnd());

            Console.WriteLine("Object insert response");
            Console.WriteLine(jsonResponse.ToString());

            return $"{issuerId}.{objectSuffix}";
        }

        //Обновление объекта
        public string PatchObject(string issuerId, string objectSuffix) {
            Stream responseStream = _service.Loyaltyobject
                .Get($"{issuerId}.{objectSuffix}")
                .ExecuteAsStream();

            StreamReader responseReader = new StreamReader(responseStream);
            JObject jsonResponse = JObject.Parse(responseReader.ReadToEnd());

            if (jsonResponse.ContainsKey("error"))
            {
                if (jsonResponse["error"].Value<int>("code") == 404)
                {
                    Console.WriteLine($"Object {issuerId}.{objectSuffix} not found!");
                    return $"{issuerId}.{objectSuffix}";
                }
                else
                {
                    Console.WriteLine(jsonResponse.ToString());
                    return $"{issuerId}.{objectSuffix}";
                }
            }

            LoyaltyObject existingObject = JsonConvert.DeserializeObject<LoyaltyObject>(jsonResponse.ToString());

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
                patchBody.LinksModuleData = new LinksModuleData
                {
                    Uris = new List<Google.Apis.Walletobjects.v1.Data.Uri>()
                };
            }
            else
            {
                patchBody.LinksModuleData = existingObject.LinksModuleData;
            }
            patchBody.LinksModuleData.Uris.Add(newLink);

            responseStream = _service.Loyaltyobject
                .Patch(patchBody, $"{issuerId}.{objectSuffix}")
                .ExecuteAsStream();

            responseReader = new StreamReader(responseStream);
            jsonResponse = JObject.Parse(responseReader.ReadToEnd());

            Console.WriteLine("Object patch response");
            Console.WriteLine(jsonResponse.ToString());

            return $"{issuerId}.{objectSuffix}";
        }
    }
}
