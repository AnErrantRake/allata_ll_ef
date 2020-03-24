using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations.Models
{
    public partial class Death
    {
        public Death()
        {
            Person = new HashSet<Person>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("death_date", TypeName = "datetime")]
        public DateTime DeathDate { get; set; }
        [Column("death_desc")]
        public string DeathDesc { get; set; }

        [InverseProperty("Death")]
        public virtual ICollection<Person> Person { get; set; }
    }
}
