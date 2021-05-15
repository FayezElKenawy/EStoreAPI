using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        readonly Type _validatortype;

        public ValidationAspect(Type validatortype)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatortype))
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }

            _validatortype = validatortype;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatortype);
            var entityType = _validatortype.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
