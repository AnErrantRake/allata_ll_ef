using System;
using System.Linq;
using EFAnnotations.Models;

namespace EFAnnotations
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // CREATE/READ
            var dbContext = new PersonHistoryContext();
            var johnBirth = new Birth
            {
                Id = 1234, // not using generated IDs -> defaults to 0
                BirthDate = DateTime.Now
            };
            var john = new Person
            {
                Id = 1234, // not using generated IDs -> defaults to 0
                NameFirst = "John",
                NameLast = "Doe",
                Birth = johnBirth
            };
            dbContext.Add(johnBirth);
            dbContext.Add(john);
            
            var newJohn = dbContext.Person
                .FirstOrDefault();
            
            dbContext.SaveChanges();
            
            newJohn = dbContext.Person
                .FirstOrDefault();

            
            // UPDATE
            
            var misterDoeBirth = new Birth
            {
                Id = 1233,
                BirthDate = (DateTime.Now).AddYears(-30).AddDays(3)
            };
            var misterDoe = new Person
            {
                Id = 1233,
                NameFirst = "Johnald",
                NameLast = "Doe",
                Birth = misterDoeBirth 
            };
            
            newJohn.Father = misterDoe;
            dbContext.SaveChanges();
            
            newJohn = dbContext.Person
                .FirstOrDefault(person => person.Id == 1234);
            Console.WriteLine( newJohn != null ? newJohn.Father.NameFirst : "Luke is not your father");
            
            
            // DELETE
            
            dbContext.Person
                .Remove(newJohn);
            dbContext.SaveChanges();
            
            var johnQuestionMark = dbContext.Person
                .FirstOrDefault(person => person.Id == 1234);
            Console.WriteLine( johnQuestionMark != null ? "JOHN! You're alive!" : ":rip:");
        }
    }
}
