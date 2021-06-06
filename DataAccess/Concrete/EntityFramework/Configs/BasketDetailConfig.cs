using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Configs
{
    public class BasketDetailConfig : IEntityTypeConfiguration<BasketDetail>
    {
        public void Configure(EntityTypeBuilder<BasketDetail> builder)
        {
            builder.Navigation(bd => bd.Basket).AutoInclude();
        }
    }
}
