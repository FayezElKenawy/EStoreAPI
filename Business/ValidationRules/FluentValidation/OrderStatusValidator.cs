using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class OrderStatusValidator : AbstractValidator<OrderStatus>
    {
        public OrderStatusValidator()
        {
            RuleFor(os => os.Name).NotEmpty();
            RuleFor(os => os.Name).MinimumLength(2);
            RuleFor(os => os.Name).MaximumLength(20);
        }
    }
}
