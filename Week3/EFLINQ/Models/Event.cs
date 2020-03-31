using System;
using System.Collections.Generic;

namespace EFFluidAPI.Models
{
    public partial class Event
    {
        public Event()
        {
            PersonAttendedEvent = new HashSet<PersonAttendedEvent>();
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventDesc { get; set; }
        public DateTime EventDate { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<PersonAttendedEvent> PersonAttendedEvent { get; set; }
    }
}
