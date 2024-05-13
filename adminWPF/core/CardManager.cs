using Google.Apis.Walletobjects.v1.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Google.Apis.Walletobjects.v1;

namespace adminWPF.core
{
    internal class CardManager
    {
        private readonly WalletobjectsService _service;

        public CardManager(WalletobjectsService service)
        {
            _service = service;
        }

        public string PatchObject(string issuerId, string objectSuffix)
        {
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

