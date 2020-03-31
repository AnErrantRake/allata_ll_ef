using System;
using System.Collections.Generic;

namespace EFFluidAPI.Models
{
    public partial class Location
    {
        public Location()
        {
            Event = new HashSet<Event>();
            PersonLivedAtLocation = new HashSet<PersonLivedAtLocation>();
        }

        public int Id { get; set; }
        public double CoordLat { get; set; }
        public double CoordLon { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateAbbr { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<PersonLivedAtLocation> PersonLivedAtLocation { get; set; }
    }
}
