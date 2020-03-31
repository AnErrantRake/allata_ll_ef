using System;
using System.Collections.Generic;

namespace EFFluidAPI.Models
{
    public partial class Person
    {
        public Person()
        {
            InverseFather = new HashSet<Person>();
            InverseMother = new HashSet<Person>();
            PersonAttendedEvent = new HashSet<PersonAttendedEvent>();
            PersonLivedAtLocation = new HashSet<PersonLivedAtLocation>();
        }

        public int Id { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string NameMiddle { get; set; }
        public int BirthId { get; set; }
        public int? DeathId { get; set; }
        public int? FatherId { get; set; }
        public int? MotherId { get; set; }

        public virtual Birth Birth { get; set; }
        public virtual Death Death { get; set; }
        public virtual Person Father { get; set; }
        public virtual Person Mother { get; set; }
        public virtual ICollection<Person> InverseFather { get; set; }
        public virtual ICollection<Person> InverseMother { get; set; }
        public virtual ICollection<PersonAttendedEvent> PersonAttendedEvent { get; set; }
        public virtual ICollection<PersonLivedAtLocation> PersonLivedAtLocation { get; set; }
    }
}
