using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Brand : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}