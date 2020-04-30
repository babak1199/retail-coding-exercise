using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetailCodingExercise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RetailCodingExcerciseTests.Models
{
    [TestClass]
    public class SearchModelTest
    {
        [TestMethod("Search query - Required validation test")]
        public void TestQueryRequiredValidation()
        {
            var model = new Search
            {
                Query = null,
            };

            Assert.IsTrue(ValidateModel(model).Any(
                v => v.MemberNames.Contains("Query") &&
                     v.ErrorMessage.Contains("empty")));
        }

        [TestMethod("Search query - Minimum length test")]
        public void TestQueryMinimumLength()
        {
            var model = new Search
            {
                Query = "a",
            };

            Assert.IsTrue(ValidateModel(model).Any(
                v => v.MemberNames.Contains("Query") &&
                     v.ErrorMessage.Contains("at least 2 characters")));
        }

        [TestMethod("Search query - Valid query test")]
        public void TestValidQuery()
        {
            var model = new Search
            {
                Query = "fast car",
            };

            Assert.IsFalse(ValidateModel(model).Any());
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, ctx, validationResults, true);

            return validationResults;
        }
    }
}
