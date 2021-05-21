using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(uoc => uoc.UserId).NotEmpty();
            RuleFor(uoc => uoc.UserId).GreaterThan(0);
            RuleFor(uoc => uoc.OperationClaimId).NotEmpty();
            RuleFor(uoc => uoc.OperationClaimId).GreaterThan(0);
        }
    }
}
