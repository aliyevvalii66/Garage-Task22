using BigonWebShoppingApp.Models.Common;

namespace BigonWebShoppingApp.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public Category? ParentCategory { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
