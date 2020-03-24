using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations.Models
{
    public partial class Location
    {
        public Location()
        {
            Event = new HashSet<Event>();
            PersonLivedAtLocation = new HashSet<PersonLivedAtLocation>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("coord_lat")]
        public double CoordLat { get; set; }
        [Column("coord_lon")]
        public double CoordLon { get; set; }
        [Column("address1")]
        public string Address1 { get; set; }
        [Column("address2")]
        public string Address2 { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("state_abbr")]
        public string StateAbbr { get; set; }
        [Column("country")]
        public string Country { get; set; }

        [InverseProperty("Location")]
        public virtual ICollection<Event> Event { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<PersonLivedAtLocation> PersonLivedAtLocation { get; set; }
    }
}
