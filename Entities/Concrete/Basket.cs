using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Basket : BaseEntity, IEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
    }
}