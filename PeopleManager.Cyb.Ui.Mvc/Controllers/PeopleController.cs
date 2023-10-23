using Microsoft.AspNetCore.Mvc;
using PeopleManager.Cyb.Ui.Mvc.Core;
using PeopleManager.Cyb.Ui.Mvc.Models;

namespace PeopleManager.Cyb.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PeopleController(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var people = _dbContext.People.ToList();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _dbContext.People.FirstOrDefault(p => p.Id == id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            var dbPerson = _dbContext.People.FirstOrDefault(p => p.Id == person.Id);

            if (dbPerson is null)
            {
                return RedirectToAction("Index");
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
