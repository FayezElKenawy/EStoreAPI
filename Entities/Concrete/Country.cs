using Core.Entities;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Country : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}
