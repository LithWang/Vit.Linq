﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vit.Core.Module.Serialization;
using Vit.Linq.FilterRules;
using Vit.Linq.FilterRules.ComponentModel;

namespace Vit.Linq.MsTest.FilterRules
{
    [TestClass]
    public class FilterRuleConvert_Test
    {
        public class Person
        {
            public int Age { get; set; }
            public string Name { get; set; }

            public List<string> Names { get; set; }
        }


        [TestMethod]
        public void Test()
        {
            {
                Expression<Func<Person, bool>> predicate = x => (x.Age > 5 || x.Name.Contains("lith")) && (x.Age < 10 || x.Name == "lith2");

                FilterRule rule = FilterGenerateService.Instance.ConvertToData(predicate);
                var str = Json.Serialize(rule);
                rule = Json.Deserialize<FilterRule>(str);

                var strRule = "{'condition':'and','rules':[{'condition':'or','rules':[{'field':'Age','operator':'>','value':5},{'field':'Name','operator':'Contains','value':'lith'}]},{'condition':'or','rules':[{'field':'Age','operator':'<','value':10},{'field':'Name','operator':'=','value':'lith2'}]}]}"
                       .Replace("'", "\"");
                var expectedRule = Json.Deserialize<FilterRule>(strRule);

                Assert.AreEqual(expectedRule, rule);
            }


            {
                var strRule = "{'condition':'and','rules':[{'condition':'or','rules':[{'field':'Age','operator':'>','value':5},{'field':'Name','operator':'Contains','value':'lith'}]},{'condition':'or','rules':[{'field':'Age','operator':'<','value':10},{'field':'Name','operator':'=','value':'lith2'}]}]}"
                     .Replace("'", "\"");
                var expectedRule = Json.Deserialize<FilterRule>(strRule);

                Expression<Func<Person, bool>> predicate = FilterService.Instance.ConvertToCode_PredicateExpression<Person>(expectedRule);


                FilterRule rule = FilterGenerateService.Instance.ConvertToData(predicate);
                var str = Json.Serialize(rule);
                rule = Json.Deserialize<FilterRule>(str);

                //Assert.AreEqual(expectedRule, rule);
            }
        }




    }
}
