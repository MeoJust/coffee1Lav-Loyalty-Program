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
            };

            LoyaltyObject newObject = new LoyaltyObject
            {
                Id = $"{issuerId}.{objectSuffix}",
                ClassId = $"{issuerId}.{classSuffix}",
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
