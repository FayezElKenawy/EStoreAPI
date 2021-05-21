using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(oc => oc.Name).NotEmpty();
            RuleFor(oc => oc.Name).MinimumLength(2);
            RuleFor(oc => oc.Name).MaximumLength(100);
        }
    }
}
