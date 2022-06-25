using System;
using System.Collections.Generic;

#nullable disable

namespace AIS_Practice.Entities
{
    public partial class Sale
    {
        public Sale()
        {
            Katalogs = new HashSet<Katalog>();
        }

        public int Id { get; set; }
        public int Staff { get; set; }
        public int Product { get; set; }
        public DateTime Date { get; set; }

        public virtual Katalog ProductNavigation { get; set; }
        public virtual staff StaffNavigation { get; set; }
        public virtual ICollection<Katalog> Katalogs { get; set; }
    }
}
