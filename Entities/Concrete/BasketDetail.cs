using Core.Entities;
using Entities.Abstract;
using Newtonsoft.Json;

namespace Entities.Concrete
{
    public class BasketDetail : BaseEntity, IEntity
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public Basket Basket { get; set; }
    }
}