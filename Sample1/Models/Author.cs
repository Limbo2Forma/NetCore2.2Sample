using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample1.Models {
    public class Author {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}