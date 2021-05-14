using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class City : BaseEntity, IEntity
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}