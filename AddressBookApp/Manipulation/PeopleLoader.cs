using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using AddressBookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AddressBookApp.Manipulation
{
    public class PeopleLoader : IpeopleLoader
    {
        private IList<Person> Collection { get; set; }
        string filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory)) + @"\wwwroot\lib\people.json";

        public PeopleLoader()
        {
            string _peopleJson = File.ReadAllText(filePath);
            var _people = JsonConvert.DeserializeObject<List<Person>>(_peopleJson);
            this.Collection = _people;

        }
        public Person Get(int id)
        {
            var person = this.Collection.SingleOrDefault(p => p.Id == id);

            return person;
        }

        public IEnumerable<Person> GetAll()
        {
            return this.Collection;
        }

        public Person Edit(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException();
            }

            var existing = this.Collection.SingleOrDefault(p => p.Id == person.Id);

            if (existing != null)
            {
                this.Collection = this.Collection.Except(new List<Person> { existing }).ToList();
                this.Collection.Add(person);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(this.Collection));

                existing = person;
            }

            return existing;
        }


        public void Create(Person person)
        {
            //add new person to the list
            this.Collection.Add(person);
            //save back to file so it's all updated on the json as well
            File.WriteAllText(filePath, JsonConvert.SerializeObject(this.Collection));
        }
        public void Delete(Person person)
        {
            //add new person to the list
            this.Collection.Remove(person);
            //save back to file so it's all updated on the json as well
            File.WriteAllText(filePath, JsonConvert.SerializeObject(this.Collection));

        }
    }
}
