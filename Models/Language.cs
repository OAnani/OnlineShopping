using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Language
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
