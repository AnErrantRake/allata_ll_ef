using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations.Models
{
    public partial class Birth
    {
        public Birth()
        {
            Person = new HashSet<Person>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("birth_date", TypeName = "datetime")]
        public DateTime BirthDate { get; set; }
        [Column("birth_desc")]
        public string BirthDesc { get; set; }

        [InverseProperty("Birth")]
        public virtual ICollection<Person> Person { get; set; }
    }
}
