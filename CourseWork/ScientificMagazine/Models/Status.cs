using System.ComponentModel.DataAnnotations;

namespace ScientificMagazine.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}
