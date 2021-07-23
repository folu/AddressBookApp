using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookApp.Models;


namespace AddressBookApp.Manipulation
{
    public interface IpeopleLoader
    {
        public Person Get(int id);
        IEnumerable<Person> GetAll();
        public Person Edit(Person person);
        public void Create(Person person);
        public void Delete(Person person);
    }
}
