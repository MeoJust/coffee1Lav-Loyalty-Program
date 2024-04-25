using Google.Apis.Auth.OAuth2;
using Google.Apis.Walletobjects.v1.Data;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace aspApp
{
    //Генерация JWT
    public class JWTGen
    {
        private readonly ServiceAccountCredential _credentials;

        public JWTGen(ServiceAccountCredential credentials) {
            _credentials = credentials;
        }

        public string CreateJWT(string issuerId, string classSuffix, string objectSuffix) {
            JsonSerializerSettings excludeNulls = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

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

            JObject serializedClass = JObject.Parse(
                JsonConvert.SerializeObject(newClass, excludeNulls));
            JObject serializedObject = JObject.Parse(
                JsonConvert.SerializeObject(newObject, excludeNulls));

            JObject jwtPayload = JObject.Parse(JsonConvert.SerializeObject(new
            {
                iss = _credentials.Id,
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

            JwtPayload claims = JwtPayload.Deserialize(jwtPayload.ToString());

            RsaSecurityKey key = new RsaSecurityKey(_credentials.Key);
            SigningCredentials signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.RsaSha256);
            JwtSecurityToken jwt = new JwtSecurityToken(
                new JwtHeader(signingCredentials), claims);
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            Console.WriteLine("Add to Google Wallet link");
            Console.WriteLine($"https://pay.google.com/gp/v/save/{token}");

            return $"https://pay.google.com/gp/v/save/{token}";
        }
    }
}
