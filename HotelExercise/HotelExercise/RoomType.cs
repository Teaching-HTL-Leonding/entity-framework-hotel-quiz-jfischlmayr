using HotelExercise;
using System.ComponentModel.DataAnnotations;

class RoomType
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        public int Size { get; set; }
        public bool DisabilityAccesible { get; set; }
        public int Amount { get; set; }
}
