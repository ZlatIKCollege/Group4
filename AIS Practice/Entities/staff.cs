using System;
using System.Collections.Generic;

#nullable disable

namespace AIS_Practice.Entities
{
    public partial class staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime? Birthday { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int Passport { get; set; }
    }
}
