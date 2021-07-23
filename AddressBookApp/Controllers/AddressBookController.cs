using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookApp.Manipulation;
using AddressBookApp.Models;
using Newtonsoft.Json.Linq;

namespace AddressBookApp.Controllers
{

    public class AddressBookController : Controller
    {
        public AddressBookController(IpeopleLoader peopleLoader)
        {
            this._PeopleLoader = peopleLoader;
        }
        private IpeopleLoader _PeopleLoader { get; }


        public IActionResult Index()
        {
            var people = this._PeopleLoader.GetAll();
            if (people != null)
            {
                return View(people);
            }
            return View(); //maybe view error page here?
        }

        public IActionResult Edit(int id)
        {
            //code to edit here
            return View();

        }

        public IActionResult Delete(int id)
        {
            //code to delete here
            return View();

        }

        public IActionResult Details(int id)
        {
            //code to view person details
            var person = this._PeopleLoader.Get(id);

            if (person == null)
            {
                return NotFound(id);
            }
            return View(person);

        }

        public IActionResult Create()
        {
            //This will be get 
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person NewPerson)
        {
            //This will be post
            //code to Create here
            //before this new person can be added we need to give them an id
            var allPeople = this._PeopleLoader.GetAll().ToList();
            //get most recent record which will have the higest id
            int max = allPeople.Count;
            int newID = allPeople[max-1].Id+1; //max-1 as array starts from 0
            NewPerson.Id = newID;
            JObject obj = (JObject)JToken.FromObject(NewPerson);
            this._PeopleLoader.Create(NewPerson);
            return View();

        }
    }
}
