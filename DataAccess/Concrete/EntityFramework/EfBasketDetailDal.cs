using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDetailDal : EfEntityRepositoryBase<BasketDetail, EStoreContext>, IBasketDetailDal
    {
        public List<BasketDetailDto> GetAllBasketDetailDtos(Expression<Func<BasketDetailDto, bool>> expression = null)
        {
            using (EStoreContext context = new EStoreContext())
            {
                var result = from bd in context.BasketDetails
                             join b in context.Baskets
                             on bd.BasketId equals b.Id
                             join u in context.Users
                             on b.UserId equals u.Id
                             select new BasketDetailDto
                             {
                                 Id = bd.Id,
                                 BasketId = b.Id,
                                 UserId = b.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 ProductId = bd.ProductId,
                                 Count = bd.Count,
                                 CreateDate = bd.CreateDate,
                                 Active = bd.Active
                             };

                return expression == null ? result.ToList() : result.Where(expression).ToList();

            }
        }
    }
}
