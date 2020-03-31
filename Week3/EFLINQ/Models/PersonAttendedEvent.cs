using System;
using System.Collections.Generic;

namespace EFFluidAPI.Models
{
    public partial class PersonAttendedEvent
    {
        public int PersonId { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Person Person { get; set; }
    }
}
