using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Category : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}