using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class OrderStatus : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}