using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, EStoreContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (EStoreContext context = new EStoreContext())
            {
                var result = from u in context.Users
                             join uoc in context.UserOperationClaims
                             on u.Id equals uoc.UserId
                             join oc in context.OperationClaims
                             on uoc.OperationClaimId equals oc.Id
                             where user.Id == uoc.UserId
                             select new OperationClaim
                             {
                                 Id = oc.Id,
                                 Name = oc.Name
                             };

                return result.ToList();
            }
        }
    }
}
