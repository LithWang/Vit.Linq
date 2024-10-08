﻿using System;
using System.Linq.Expressions;

using Vit.Linq.FilterRules.ComponentModel;

namespace Vit.Linq.FilterRules.FilterConvertor
{
    public class OperatorConvertArgument
    {
        public FilterService filterService { get; set; }


        public ParameterExpression parameter { get; set; }


        public IFilterRule filter { get; set; }

        public Expression leftValueExpression { get; set; }
        public Type leftValueType { get; set; }
        public string Operator { get; set; }

        public StringComparison comparison { get; set; }


    }
}
