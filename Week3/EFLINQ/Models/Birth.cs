using System;
using System.Collections.Generic;

namespace EFFluidAPI.Models
{
    public partial class Birth
    {
        public Birth()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDesc { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
