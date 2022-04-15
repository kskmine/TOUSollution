using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcContact.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcContact.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IRepositoryPerson _personRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _personRepository = RepositoryFactory.CreateRepo("PERSON");
        }

        public IActionResult Index()
        {
            IRepositoryPerson _repository = RepositoryFactory.CreateRepo("PERSON");
            List<Person> liste =_repository.List();
            

            //basit haliyle model görüntüleme
            //KisiTest mdl = new KisiTest() { Soyad="Ad",Ad="Soyad"};
            //mdl.Ad = "Emine";
            //mdl.Soyad = "Kosak";
            return View(liste);
        }

        [HttpGet]
        public IActionResult CreatePerson(int? id)//? şey demek null olabilir demek
        {
            Person model = new Person();
            if (id.HasValue && id>0)
            {
                List<Person> people = _personRepository.List();
                model = people.First(c => c.Id == id);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            _personRepository.AddOrUpdate(person);
            return RedirectToAction("Index");
            

    
        }

        public IActionResult Delete(int id)
        {
            _personRepository.Delete(id);
            return RedirectToAction("Index");
        }


        public IActionResult Privacy(KisiTest kisi)
        {
            return View(kisi);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
