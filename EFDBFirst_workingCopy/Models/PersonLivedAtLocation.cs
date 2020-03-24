using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations.Models
{
    [Table("PersonLived_AtLocation")]
    public partial class PersonLivedAtLocation
    {
        [Key]
        [Column("person_id")]
        public int PersonId { get; set; }
        [Key]
        [Column("location_id")]
        public int LocationId { get; set; }
        [Column("from_date", TypeName = "datetime")]
        public DateTime FromDate { get; set; }
        [Column("to_date", TypeName = "datetime")]
        public DateTime? ToDate { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("PersonLivedAtLocation")]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty("PersonLivedAtLocation")]
        public virtual Person Person { get; set; }
    }
}
