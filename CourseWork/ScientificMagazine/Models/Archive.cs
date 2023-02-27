using System.ComponentModel.DataAnnotations;

namespace ScientificMagazine.Models
{
    public class Archive
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Magazine { get; set; }
    }
}
