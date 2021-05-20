using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.UserId).NotEmpty();
            RuleFor(a => a.UserId).GreaterThan(0);
            RuleFor(a => a.CityId).NotEmpty();
            RuleFor(a => a.CityId).GreaterThan(0);
            RuleFor(a => a.AddressDetail).NotEmpty();
            RuleFor(a => a.AddressDetail).MinimumLength(2);
            RuleFor(a => a.AddressDetail).MaximumLength(50);
            RuleFor(a => a.PostalCode).NotEmpty();
            RuleFor(a => a.PostalCode).MinimumLength(2);
            RuleFor(a => a.PostalCode).MaximumLength(20);
        }
    }
}
