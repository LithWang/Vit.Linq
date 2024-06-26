﻿using System;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vit.Core.Module.Serialization;
using Vit.Linq.ExpressionTree.ComponentModel;
using Vit.Linq.ExpressionTree.ExpressionTreeTest;

namespace Vit.Linq.ExpressionTree.MsTest.ExpressionTreeTest
{
    [TestClass]
    public class ExpressionNode_Test
    {

        [TestMethod]
        public void TestQueryable()
        {
            ExpressionTester.TestQueryable(GetQuery());
        }


        [TestMethod]
        public void TestEnumerable()
        {
            ExpressionTester.TestEnumerable(GetQuery());
        }



        static IQueryable<ExpressionTester.User> GetQuery()
        {
            var convertService = ExpressionConvertService.Instance;
            var queryTypeName = "TestQuery";
            var sourceData = ExpressionTester.GetSourceData().AsQueryable();

            Func<Expression, Type, object> QueryExecutor = (expression, type) =>
            {
                ExpressionNode node;

                // #1 Code to Data
                // query => query.Where().OrderBy().Skip().Take().Select().ToList();
                var isArgument = QueryableBuilder.QueryTypeNameCompare(queryTypeName);
                node = convertService.ConvertToData(expression, autoReduce: true, isArgument: isArgument);
                var strNode = Json.Serialize(node);

                // #2 Data to Code
                // query => query.Where(person => (person.id >= 10))
                var lambdaExp = convertService.ToLambdaExpression(node, typeof(IQueryable<ExpressionTester.User>));
                //var predicate_ = lambdaExp.Compile();
                var exp3 = (Expression<Func<IQueryable<ExpressionTester.User>, IQueryable<ExpressionTester.User>>>)lambdaExp;
                var predicate = exp3.Compile();

                return predicate(sourceData);
            };

            return QueryableBuilder.Build<ExpressionTester.User>(QueryExecutor, queryTypeName);
        }



    }
}
