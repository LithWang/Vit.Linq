﻿using System;
using System.Linq.Expressions;



namespace Vit.Linq.ExpressionNodes.ComponentModel
{

    public interface ExpressionNode_Lambda : IExpressionNode
    {
        public string[] parameterNames { get; }

        public ExpressionNode body { get; }

        Type[] Lambda_GetParamTypes();
        Type Lambda_GetReturnType();
        ExpressionNode Lambda_SetParamTypes(Type[] paramTypes, Type returnType = null);

        LambdaExpression Lambda_GetLambdaExpression();
        ExpressionNode Lambda_SetLambdaExpression(LambdaExpression lambda);
    }

    public partial class ExpressionNode : ExpressionNode_Lambda
    {
        public string[] parameterNames { get; set; }

        public ExpressionNode body { get; set; }

        public static ExpressionNode_Lambda Lambda(string[] parameterNames, ExpressionNode body)
        {
            return new ExpressionNode
            {
                nodeType = NodeType.Lambda,
                parameterNames = parameterNames,
                body = body,
            };
        }
        public Type[] Lambda_GetParamTypes()
        {
            return GetCodeArg("Lambda_ParamTypes") as Type[];
        }
        public Type Lambda_GetReturnType()
        {
            return GetCodeArg("Lambda_ReturnType") as Type;
        }

        public ExpressionNode Lambda_SetParamTypes(Type[] paramTypes, Type returnType = null)
        {
            if (paramTypes != null)
                SetCodeArg("Lambda_ParamTypes", paramTypes);
            if (returnType != null)
                SetCodeArg("Lambda_ReturnType", returnType);
            return this;
        }


        public LambdaExpression Lambda_GetLambdaExpression()
        {
            return GetCodeArg("Lambda_LambdaExpression") as LambdaExpression;
        }
        public ExpressionNode Lambda_SetLambdaExpression(LambdaExpression lambda)
        {
            if (lambda != null)
                SetCodeArg("Lambda_LambdaExpression", lambda);
            return this;
        }


    }
}
