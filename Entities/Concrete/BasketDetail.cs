using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class BasketDetail : BaseEntity, IEntity
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}