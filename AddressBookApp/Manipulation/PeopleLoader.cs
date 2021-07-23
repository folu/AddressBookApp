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
            //var webClient = new WebClient();
            //var json = webClient.DownloadString(@"C:\Users\KASIMOTO SATOSHI\Projects\Todo\AddressBook_webApp\AddressBook_webApp\wwwroot\lib\people.json");
            //string jsonn = File.ReadAllText("myobjects.json");
           // Person ObjPeopleList = new IList<Person>;
            //ObjPeopleList = JsonConvert.DeserializeObject<Person>(json);
            // Person myDeserializedClass = JsonConvert.DeserializeObject<Person>(json);
            //this.Collection = myDeserializedClass;


            //string filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory)) + @"\wwwroot\lib\people.json";
            string _peopleJson = File.ReadAllText(filePath);
            var _people = JsonConvert.DeserializeObject<List<Person>>(_peopleJson);
            this.Collection = _people;
            //if (json != null)
            //{
            //    var people = JsonConvert.DeserializeObject<List<Person>>(json)
            //    this.Collection = people;
            //}

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
            //code to edit here
            return null;
        }


        public Boolean Create(Person person)
        {
            //add new person to the list
            this.Collection.Add(person);
            //save back to file so it's all updated on the json as well
            File.WriteAllText(filePath, JsonConvert.SerializeObject(this.Collection));
            return true;
        }
    }
}
