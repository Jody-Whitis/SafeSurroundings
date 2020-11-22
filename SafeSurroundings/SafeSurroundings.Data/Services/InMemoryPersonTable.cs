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
            persons = new List<Person> { new Person { ID=1,DisplayName="Jody", isPrivate=false}, new Person {ID=2,DisplayName="Scruffy",isPrivate = false } };
        }

        public IEnumerable<Person> GetAll()
        {
            return persons.OrderBy(p => p.ID);
        }
    }
}
