using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations.Models
{
    [Table("PersonAttended_Event")]
    public partial class PersonAttendedEvent
    {
        [Key]
        [Column("person_id")]
        public int PersonId { get; set; }
        [Key]
        [Column("event_id")]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        [InverseProperty("PersonAttendedEvent")]
        public virtual Event Event { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty("PersonAttendedEvent")]
        public virtual Person Person { get; set; }
    }
}
