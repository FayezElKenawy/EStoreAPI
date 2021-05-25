using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Basket : BaseEntity, IEntity
    {
        public int UserId { get; set; }
    }
}