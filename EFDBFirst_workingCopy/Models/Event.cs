using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations.Models
{
    public partial class Event
    {
        public Event()
        {
            PersonAttendedEvent = new HashSet<PersonAttendedEvent>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("event_name")]
        public string EventName { get; set; }
        [Required]
        [Column("event_desc")]
        public string EventDesc { get; set; }
        [Column("event_date", TypeName = "datetime")]
        public DateTime EventDate { get; set; }
        [Column("location_id")]
        public int LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("Event")]
        public virtual Location Location { get; set; }
        [InverseProperty("Event")]
        public virtual ICollection<PersonAttendedEvent> PersonAttendedEvent { get; set; }
    }
}
