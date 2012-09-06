using System.Collections.Generic;
using MVVMDemo.Model;

namespace MVVMDemo.Data
{
    public class PersonRepository
    {
        public IList<Person> GetAll()
        {
            // would really retrieve from data source
            return new List<Person>{
                new Person{FirstName="Fernando", LastName="Alonso"},
                new Person{FirstName="Felipe", LastName="Massa"}
            };
        }

        public void Save(Person person)
        {
             // would really save to data source  
        }
    }
}
