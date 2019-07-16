using System.ComponentModel.DataAnnotations;

namespace Sample1.Models {
    public class Author {
        public long AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}