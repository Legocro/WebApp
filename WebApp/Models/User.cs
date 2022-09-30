namespace WebApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public Company company { get; set; }


    }

    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public Coordinates geo { get; set; }

    }

    public class Coordinates
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }
    public class Company
    {
            public string name { get; set;}
            public string catchPhrase { get; set; }
            public string bs { get; set; }
    }
}
