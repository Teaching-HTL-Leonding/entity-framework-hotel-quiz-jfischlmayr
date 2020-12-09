using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelExercise
{
    class Hotel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        private string Street { get; set; }
        private int ZIPCode { get; set; }
        [MaxLength(50)]
        private string City { get; set; }
        public string Address { get { return Street + ZIPCode + City; } }
    }
}
