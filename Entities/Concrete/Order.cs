using Core.Entities;
using Core.Entities.Concrete;
using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Order : BaseEntity, IEntity
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int OrderStatusId { get; set; }

        //Navigation properties 
        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
    }
}