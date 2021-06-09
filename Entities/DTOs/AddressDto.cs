using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AddressDto : IDto
    {
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string AddressDetail { get; set; }
        public string PostalCode { get; set; }
    }
}
