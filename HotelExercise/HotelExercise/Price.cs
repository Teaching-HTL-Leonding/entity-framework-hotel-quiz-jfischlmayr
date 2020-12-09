using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelExercise
{
    class Price
    {
        public int Id { get; set; }
        public int RoomTypeID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public double PricePerNight { get; set; }
    }
}
