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
            //get
            var person = this._PeopleLoader.Get(id);

            if (person != null)
            {
                //found
                return View(person);
            }
            return View();

        }
        [HttpPost]
        public IActionResult Edit(int id, Person NewPerson)
        {
            //get our current person

            if (id != NewPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this._PeopleLoader.Edit(NewPerson);
                return RedirectToAction(nameof(Index));
            }

            //found
           
           return View(NewPerson);

        }

        public IActionResult Delete(int id)
        {
            var person = this._PeopleLoader.Get(id);

            if (person != null)
            {
                //found
                return View(person);
            }
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = this._PeopleLoader.Get(id);

            if (person != null)
            {
                this._PeopleLoader.Delete(person);
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        public IActionResult Details(int id)
        {
            //get
            var person = this._PeopleLoader.Get(id);

            if (person != null)
            {
                //found
                return View(person);
            }
            return View();

        }
        [HttpPost]
        public IActionResult Details(int id,Person p)
        {
            //code to view person details
            var person = this._PeopleLoader.Get(id);

            if (person == null)
            {
                return NotFound(id);
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(person);

        }

        public IActionResult Create()
        {
            //This will be get 
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person NewPerson)
        {
            //This will be post
            //code to Create here
            //before this new person can be added we need to give them an id
            //sort the array by uniqueID
            var allPeople = this._PeopleLoader.GetAll().OrderBy(p => p.Id).ToList();
            //get most recent record which will have the higest id
            int max = allPeople.Count;
            int newID = allPeople[max-1].Id+1; //max-1 as array starts from 0
            NewPerson.Id = newID;
            JObject obj = (JObject)JToken.FromObject(NewPerson);
            this._PeopleLoader.Create(NewPerson);
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
    }
}
