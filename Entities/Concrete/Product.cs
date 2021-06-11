using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Product : BaseEntity, IEntity
    {
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        //Navigation properties

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}