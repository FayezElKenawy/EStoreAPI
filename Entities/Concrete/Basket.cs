using Core.Entities;
using Entities.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Basket : BaseEntity, IEntity
    {
        public Basket()
        {
            BasketDetails = new List<BasketDetail>();
        }


        public int UserId { get; set; }

        public ICollection<BasketDetail> BasketDetails { get; set; }
    }
}