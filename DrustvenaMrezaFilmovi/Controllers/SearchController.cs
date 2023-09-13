using DrustvenaMrezaFilmovi.Data;
using DrustvenaMrezaFilmovi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrustvenaMrezaFilmovi.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly AppDbContext db;

        public SearchController(ILogger<SearchController> logger, AppDbContext _db)
        {
            _logger = logger;
            db = _db;

        }
        [HttpGet]
        [Route("api/search")]
        public IActionResult GetSearchSuggestions(string query)
        {
            // Fetch search suggestions based on the query
            // Return the suggestions as JSON data
            SearchResultHelp suggestions = new SearchResultHelp();
            suggestions.Filmovi = db.Filmovi.Where(p => p.Naziv.StartsWith(query)).Take(4).ToList();
            suggestions.Korisnici = db.Korisnici.Where(p => p.Username.StartsWith(query)).Take(4).ToList();
            suggestions.Umetnici = db.Umetnici.Where(p => p.Ime.StartsWith(query)).Take(2).ToList();
            foreach(Umetnik u in db.Umetnici.Where(p => p.Prezime.StartsWith(query)).Take(2).ToList())
                suggestions.Umetnici.Add(u);
            return Ok(suggestions);
        }
    }
}
