using System.Net;
using System.Net.Mail;

namespace WebApp.Models
{
    public class Entry
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string company { get; set; }
        public Entry(string sentName = null, string sentSurname = null, string sentEmail = null)
        {
            name = sentName;
            surname = sentSurname; 
            email = sentEmail;
        }
        public Entry ConvertDataToEntry(User data)
        {
            address = String.Format("Street: {0}  Suite: {1}  City: {2}  Zipcode: {3}  Coordinates: ({4},{5})", data.address.street, data.address.suite, data.address.city, data.address.zipcode, data.address.geo.lat, data.address.geo.lon);
            phone = data.phone;
            website = data.website;
            company = String.Format("Name: {0}  Catch phrase: {1}  BS: {2}", data.company.name, data.company.catchPhrase, data.company.bs);
            return this;
        }
        public void SaveToDatabase()
        {
            string saveCommand = String.Format("INSERT INTO Users VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", name, surname, email, address, phone, website, company);
            DB.OpenConnection();
            int rows = DB.ExecuteCommand(saveCommand);
            DB.CloseConnection();
        }

        public void SendToEmail(string emailDest)
        { 
            string emailBody = String.Format("Name:{0} {1}\n Email:{2}\nAddress:{3}\nPhone:{4}\nWebsite:{5}\nCompany:{6}", name, surname, email, address, phone, website, company);
            SmtpClient smtpClient = new SmtpClient("smtp-server")
            {
                Port = 587,
                Credentials = new NetworkCredential("user", "pass"),
                EnableSsl = true,
            };

            smtpClient.Send("posiljatelj@mail.hr", emailDest, "Unesen korisnik", emailBody);
        }
    }
}
