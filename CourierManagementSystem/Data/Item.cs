using System.ComponentModel.DataAnnotations;

namespace CourierManagementSystem.Data
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, Enter a Category first")]
        public string  Category { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int CourierItem { get; set; }
        public DateTime DateofAddingItem { get; set; }
        public Item()
        {
            DateofAddingItem = DateTime.Now.Date;
        }

    }

}
