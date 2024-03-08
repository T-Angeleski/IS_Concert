using System.ComponentModel.DataAnnotations;

namespace ConcertLab.Web.Models {
    public class Concert {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place {  get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
    }
}
