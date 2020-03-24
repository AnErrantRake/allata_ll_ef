using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations.Models
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

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name_first")]
        public string NameFirst { get; set; }
        [Required]
        [Column("name_last")]
        public string NameLast { get; set; }
        [Column("name_middle")]
        public string NameMiddle { get; set; }
        [Column("birth_id")]
        public int BirthId { get; set; }
        [Column("death_id")]
        public int? DeathId { get; set; }
        [Column("father_id")]
        public int? FatherId { get; set; }
        [Column("mother_id")]
        public int? MotherId { get; set; }

        [ForeignKey(nameof(BirthId))]
        [InverseProperty("Person")]
        public virtual Birth Birth { get; set; }
        [ForeignKey(nameof(DeathId))]
        [InverseProperty("Person")]
        public virtual Death Death { get; set; }
        [ForeignKey(nameof(FatherId))]
        [InverseProperty(nameof(Person.InverseFather))]
        public virtual Person Father { get; set; }
        [ForeignKey(nameof(MotherId))]
        [InverseProperty(nameof(Person.InverseMother))]
        public virtual Person Mother { get; set; }
        [InverseProperty(nameof(Person.Father))]
        public virtual ICollection<Person> InverseFather { get; set; }
        [InverseProperty(nameof(Person.Mother))]
        public virtual ICollection<Person> InverseMother { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<PersonAttendedEvent> PersonAttendedEvent { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<PersonLivedAtLocation> PersonLivedAtLocation { get; set; }
    }
}
