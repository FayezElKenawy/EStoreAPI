using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.AddressId).NotEmpty();
            RuleFor(o => o.AddressId).GreaterThan(0);
            RuleFor(o => o.OrderStatusId).NotEmpty();
            RuleFor(o => o.OrderStatusId).GreaterThan(0);
            RuleFor(o => o.Count).NotEmpty();
            RuleFor(o => o.Count).GreaterThan(0);
            RuleFor(o => o.UserId).NotEmpty();
            RuleFor(o => o.UserId).GreaterThan(0);
        }
    }
}
