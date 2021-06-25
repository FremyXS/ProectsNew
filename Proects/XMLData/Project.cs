using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class Project
    {
        public string Name { get; }
        public List<Person> Persons { get; private set; } = new List<Person>();
        public Project(string name)
        {
            Name = name;
        }
        public void AddPerson(Person person)
        {
            Persons.Add(person);
        }

    }
    public class Person
    {
        public string Role { get; }
        public string Name { get; }
        public Person(string name, string role)
        {
            Role = role;
            Name = name;
        }
    }
}
