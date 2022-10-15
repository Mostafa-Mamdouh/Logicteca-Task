using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Part SKU")]
        public string PartSKU { get; set; }
        [DisplayName("Band")]
        public double? Band { get; set; }
        [DisplayName("Category Code")]
        public string? CategoryCode { get; set; }
        [DisplayName("Manufacturer")]
        public string? Manufacturer { get; set; }
        [DisplayName("Item Description")]
        public string? ItemDescription { get; set; }
        [DisplayName("List Price")]
        public double? ListPrice { get; set; }
        [DisplayName("Minimum Discount")]
        public double? MinimumDiscount { get; set; }
        [DisplayName("Discounted Price")]
        public double? DiscountedPrice { get; set; }
    }
}
