using System;
using System.Collections.Generic;

#nullable disable

namespace AIS_Practice.Entities
{
    public partial class Type
    {
        public Type()
        {
            Katalogs = new HashSet<Katalog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Katalog> Katalogs { get; set; }
    }
}
