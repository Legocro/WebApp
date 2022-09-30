using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string visitInfo = String.Format("Posjecena stranica:{0} u {1}", ViewData["title"], DateTime.Now.ToLocalTime());
            _logger.LogInformation(visitInfo);
        }

        public void OnPost()
        {
            string postInfo = String.Format("Poslani podatci: {{ime: {0}, prezime: {1}, email: {2}}}", Request.Form["ime"], Request.Form["prezime"], Request.Form["email"]);
            _logger.LogInformation(postInfo);
            string name = Request.Form["ime"];
            string surname = Request.Form["prezime"];
            string email = Request.Form["email"];
            new DataManager(name, surname, email);
        }
    }
}