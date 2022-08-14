using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Services
{

    public class InMemoryPersonTable
    {
        List<Person> persons;

        public InMemoryPersonTable()
        {
            persons = new List<Person> { new Person { ID=1,DisplayName="Test", isPrivate=false}, new Person {ID=2,DisplayName="Scruffy",isPrivate = false } };
        }

        public void Add(Person newPerson)
        {
            newPerson.ID = persons.Max(p => p.ID) + 1;
            persons.Add(newPerson);
        }

        public void Delete(int id)
        {
            Person person = persons.Where(p => p.ID == id).FirstOrDefault();
            persons.Remove(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return persons.OrderBy(p => p.ID);
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
