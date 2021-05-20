using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Address : BaseEntity, IEntity
    {
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string AddressDetail { get; set; }
        public string PostalCode { get; set; }
    }
}