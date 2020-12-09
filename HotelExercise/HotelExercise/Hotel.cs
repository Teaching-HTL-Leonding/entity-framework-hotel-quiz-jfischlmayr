using System.ComponentModel.DataAnnotations;

public class Hotel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        public int ZIPCode { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        public string Address { get { return Street + ZIPCode + City; } }
    }