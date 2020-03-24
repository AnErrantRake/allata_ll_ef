using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFAnnotations
{
    class Program
    {
        static void Main(string[] args)
        {
            // construct database
            var DbContext = new PersonContext();
            DbContext.Database.EnsureCreated();
            
            // add some data
            var jim = DbContext.Add(GetNewPerson("Jim", "Doe"));
            var frank = DbContext.Add(GetNewPerson("Frank", "Perrier"));
            DbContext.SaveChanges(); // need the generated IDs from the db
            
            DbContext.Add(GetNewBirth(jim.Entity.Id));
            DbContext.Add(GetNewBirth(frank.Entity.Id));
            
            DbContext.Add(GetNewDeath(frank.Entity.Id));
            DbContext.Add(GetNewDeath(frank.Entity.Id));
            DbContext.Add(GetNewDeath(frank.Entity.Id));
            DbContext.SaveChanges();
            
            // get some data
            
            // our current frank
            frank.Entity.Deaths.ToList().ForEach(death => Console.WriteLine(death.DeathDate.ToString()));
            // what about all franks?
            DbContext.Person
                .AsNoTracking() // read only
                .Include(person => person.Deaths)
                .Include(person => person.Birth)
                .Where(person => person.NameFirst == "Frank")
                .ToList()
                .ForEach(person =>
                {
                    Console.WriteLine("{0} {1} - {2}", person.NameFirst, person.NameLast, person.Id);
                    Console.WriteLine("Born " + person.Birth.BirthDate);
                    Console.WriteLine("Died ");
                    person.Deaths
                        .ToList()
                        .ForEach(death => Console.WriteLine(death.DeathDate.ToString()));
                });
        }

        private static Person GetNewPerson(string first, string last)
        {
            return new Person
            {
                NameFirst = first,
                NameLast = last
            };
        }
        
        
        private static Birth GetNewBirth(int personId)
        {
            return new Birth
            {
                BirthDate = (DateTime.Now).AddYears(-30),
                PersonID = personId
            };
        }
        
        private static Death GetNewDeath(int personId)
        {
            return new Death
            {
                DeathDate = DateTime.Now,
                PersonID = personId
            };
        }
    }
}
