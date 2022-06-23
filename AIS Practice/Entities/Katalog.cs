using System;
using System.Collections.Generic;

#nullable disable

namespace AIS_Practice.Entities
{
    public partial class Katalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Company { get; set; }

        public virtual Type TypeNavigation { get; set; }
    }
}
