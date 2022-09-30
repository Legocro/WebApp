using System.Text.Json;
namespace WebApp.Models
{
    public class DataManager
    {
        private Entry entry;
        private string baseUrl = "http://jsonplaceholder.typicode.com/users";
        private bool foundData = false;

        private static readonly HttpClient client = new HttpClient();

        public DataManager(string name, string surname, string email)
        {
            entry = new Entry(name, surname, email);
            CheckResource();
        }

        private async Task<Entry> GetUser(string property, string value)
        {
            var result = await client.GetAsync(String.Format("{0}?{1}={2}", baseUrl, property, value));
            string serializedData = await result.Content.ReadAsStringAsync();
            try
            {
                User data = JsonSerializer.Deserialize<User>(serializedData.Substring(1, serializedData.Length - 2));
                Entry generatedEntry = new Entry("", "", "");
                return generatedEntry.ConvertDataToEntry(data);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private async Task CheckResource()
        {
            await CheckName();
            await CheckEmail();
            entry.SaveToDatabase();
            //entry.SendToEmail("primatelj@mail.hr") Nema podataka za mail (kredencije, primatelj, smtp server...)
        }

        private async Task CheckName()
        {
            Entry response = await GetUser("name", entry.name + " " + entry.surname);
            if (response != null)
            {
                foundData = true;
                response.name = entry.name;
                response.surname = entry.surname;
                response.email = entry.email;
                entry = response;
            }  


        }

        private async Task CheckEmail()
        {
            if (foundData)
                return;
            Entry response = await GetUser("email", entry.email);
            if (response != null)
            {
                foundData = true;
                response.name = entry.name;
                response.surname = entry.surname;
                response.email = entry.email;
                entry = response;
            }
        }

    }
}
