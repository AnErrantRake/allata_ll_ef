using System;
using System.Linq;
using EFFluidAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFFluidAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var _dbContext = new PersonHistoryContext();

            // method syntax
            var bornPeopleNames = _dbContext.Person
                .AsNoTracking()
                .Where(person => person.Birth.BirthDate < DateTime.Now)
                .Select(person => person.NameFirst)
                .ToList();
            
            bornPeopleNames.ForEach(name => Console.WriteLine(name));

            // query syntax
            var bornPeopleNamesQuery =
                from p in _dbContext.Person.AsNoTracking()
                where p.Birth.BirthDate < DateTime.Now
                select p.NameFirst;

            bornPeopleNamesQuery.ToList().ForEach(name => Console.WriteLine(name));
            
            // get birthday for specific person
            var johnaldBirthday = _dbContext.Person
                .AsNoTracking()
                .Where(person => person.NameFirst == "Johnald")
                .Select(person => person.Birth.BirthDate)
                .FirstOrDefault();
            Console.WriteLine(johnaldBirthday.ToString());

            // what about the whole object?
            var johnaldBirthdayObject = _dbContext.Person
                .AsNoTracking()
                .Where(person => person.NameFirst == "Johnald")
                .Select(person => person.Birth)
                .FirstOrDefault();
            Console.WriteLine(johnaldBirthdayObject.BirthDate.ToString());
            
            // what if we want Johnald AND his birthday?
            var johnaldWithBirthday = _dbContext.Person
                .AsNoTracking()
                .FirstOrDefault(person => person.NameFirst == "Johnald");

            Console.WriteLine(johnaldWithBirthday.NameFirst);
            if (johnaldWithBirthday.Birth != null)
            {
                Console.WriteLine(johnaldWithBirthday.Birth.BirthDate.ToString());
            }
            else
            {
                Console.WriteLine("Not quite");
            }
            
            // this is the way
            var johnaldWithBirth = _dbContext.Person
                .AsNoTracking()
                .Include(person => person.Birth)
                .FirstOrDefault(person => person.NameFirst == "Johnald");

            Console.WriteLine(johnaldWithBirthday.NameFirst);
            Console.WriteLine(johnaldWithBirth.Birth.BirthDate.ToString());

            // still works
            var johnaldOldSchool = _dbContext.Person
                .FromSqlRaw($"SELECT * FROM Person p WHERE p.name_first = 'Johnald'")
                .AsNoTracking()
                .Single();
            Console.WriteLine("Mr. " + johnaldOldSchool.NameLast);
            
            // not so much
            var johnaldOldSchoolQuestionMark = _dbContext.Person
                .FromSqlRaw($"SELECT p.name_first FROM Person p WHERE p.name_first = 'Johnald'")
                .AsNoTracking()
                .Single();
            Console.WriteLine("Mr. " + johnaldOldSchoolQuestionMark.NameLast);
        }
    }
}
