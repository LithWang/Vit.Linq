﻿using System.Linq.Expressions;

using Vit.Linq.FilterRules.ComponentModel;
using Vit.Linq.FilterRules.MethodCalls;

namespace Vit.Linq.FilterRules.FilterGenerator
{
    public class Lambda : IFilterGenerator
    {
        public virtual int priority { get; set; } = 400;
        public FilterRule ConvertToData(FilterGenerateArgument arg, Expression expression)
        {
            if (expression is not LambdaExpression lambda) return null;

            return arg.convertService.ConvertToData(arg, lambda.Body);
        }

    }
}
