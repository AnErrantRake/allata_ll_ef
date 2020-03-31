using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAnnotations
{
    public class Person
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NameFirst { get; set; }
        [Required]
        public string NameLast { get; set; }
        public string NameMiddle { get; set; }
        
        public virtual Birth Birth { get; set; }
        public virtual ICollection<Death> Deaths { get; set; }
    }

    public class Birth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string BirthDesc { get; set; }
        
        [Required]
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
    
    public class Death
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DeathDate { get; set; }
        public string DeathDesc { get; set; }
        
        [Required]
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}