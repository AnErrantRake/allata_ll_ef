using System;
using System.Collections.Generic;

namespace EFFluidAPI.Models
{
    public partial class PersonLivedAtLocation
    {
        public int PersonId { get; set; }
        public int LocationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual Location Location { get; set; }
        public virtual Person Person { get; set; }
    }
}
