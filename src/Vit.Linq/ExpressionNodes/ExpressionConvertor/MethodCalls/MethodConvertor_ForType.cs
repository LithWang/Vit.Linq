﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Vit.Linq.ExpressionNodes.ComponentModel;

namespace Vit.Linq.ExpressionNodes.ExpressionConvertor.MethodCalls
{

    public class MethodConvertor_ForType : MethodConvertor_Base
    {
        public MethodConvertor_ForType(Type methodType, List<string> methodNames = null, int priority = 10000)
        {
            this.methodType = methodType;
            this.methodNames = methodNames ?? methodType.GetMethods().Select(m => m.Name).ToList();
            this.priority = priority;
        }

        public Type methodType { get; protected set; }

        public List<string> methodNames { get; protected set; }
        public override int priority { get; set; }

        public override bool PredicateToData(ToDataArgument arg, MethodCallExpression call)
        {
            // is method from Queryable
            return methodType == call.Method.DeclaringType;
        }


        public override bool PredicateToCode(ToCodeArgument arg, ExpressionNode_MethodCall call)
        {
            return methodType.Name == call.methodCall_typeName && methodNames.Contains(call.methodName);
        }

        public override Expression ToCode(ToCodeArgument arg, ExpressionNode_MethodCall call)
        {
            throw new NotSupportedException($"Unsupported method typeName: {call.methodCall_typeName}, methodName: {call.methodName}");
        }
    }



}
