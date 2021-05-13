using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
