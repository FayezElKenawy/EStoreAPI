using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class BasketDetailDto : IDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
