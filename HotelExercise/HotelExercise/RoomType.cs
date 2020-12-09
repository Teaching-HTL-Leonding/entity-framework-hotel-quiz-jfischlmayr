using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelExercise
{
    class RoomType
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        [MaxLength(50)]
        public string Titel { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public int Size { get; set; }
        public bool DisabilityAccesible { get; set; }
        public int Amount { get; set; }
    }
}
