using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class BasketDetailDto : IDto
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
