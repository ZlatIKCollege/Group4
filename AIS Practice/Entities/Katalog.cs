using System;
using System.Collections.Generic;

#nullable disable

namespace AIS_Practice.Entities
{
    public partial class Katalog
    {
        public Katalog()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Company { get; set; }
        public int? Price { get; set; }

        public virtual Sale PriceNavigation { get; set; }
        public virtual Type TypeNavigation { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
