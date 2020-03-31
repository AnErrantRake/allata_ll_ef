using System;
using System.Collections.Generic;

namespace EFFluidAPI.Models
{
    public partial class Death
    {
        public Death()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public DateTime DeathDate { get; set; }
        public string DeathDesc { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
