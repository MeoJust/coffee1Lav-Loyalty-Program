using Newtonsoft.Json;
using System.IO;

namespace adminWPF.core
{
    public class GetAdminData
    {
        private readonly string _adminsFolderPath;

        public GetAdminData(string adminsFolderPath) {
            _adminsFolderPath = adminsFolderPath;
        }

        public Admin GetData(string login, string password) {
            foreach (string file in Directory.GetFiles(_adminsFolderPath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                Admin admin = JsonConvert.DeserializeObject<Admin>(jsonContent);

                if (admin.Login == login && admin.Password == password)
                {

                    return admin;
                }
            }

            return null;
        }
    }
}
