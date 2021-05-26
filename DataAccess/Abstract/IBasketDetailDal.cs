using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IBasketDetailDal : IEntityRepository<BasketDetail>
    {
        public List<BasketDetailDto> GetAllBasketDetailDtos(Expression<Func<BasketDetailDto, bool>> expression = null);
    }
}