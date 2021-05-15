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
        public decimal Price { get; set; }
    }
}