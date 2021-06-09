using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, EStoreContext>, IBasketDal
    {
        public void SetPassive(int id)
        {
            using (EStoreContext context = new EStoreContext())
            {
                Basket basket = new Basket { Id = id, Active = false };
                context.Baskets.Attach(basket);
                context.Entry(basket).Property(b => b.Active).IsModified = true;
                context.SaveChanges();
            };
        }
    }
}
