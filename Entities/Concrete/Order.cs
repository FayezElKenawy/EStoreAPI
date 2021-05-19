using Core.Entities;
using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Order : BaseEntity, IEntity
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int OrderStatusId { get; set; }
        public int Count { get; set; }
    }
}