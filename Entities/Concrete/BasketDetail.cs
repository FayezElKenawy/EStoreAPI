using Core.Entities;
using Entities.Abstract;
using Newtonsoft.Json;

namespace Entities.Concrete
{
    public class BasketDetail : BaseEntity, IEntity
    {
        public BasketDetail() { }
       
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        //Navigation properties 
        public virtual Basket Basket { get; set; }
    }
}