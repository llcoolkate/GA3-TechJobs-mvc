using System;
using Xunit;
using Moq;
using TechJobsMVCAutograded.Controllers;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded.Models;
using System.Collections.Generic;
using System.Reflection;

namespace AutogradingTests
{
    public class TestTaskThree
    {
        private readonly SearchController _controller;

        public TestTaskThree()
        {
            _controller = new SearchController();
        }

        // Test that Results method exists and contains the correct parameters
        [Fact]
        public void TestResultsExistsAndHasCorrectParameters()
        {
            MethodInfo methodInfo = typeof(SearchController).GetMethod("Results");
            ParameterInfo[] parameters = methodInfo.GetParameters();
            Assert.Equal(2, parameters.Length);
            Assert.Equal("System.String", parameters[0].ParameterType.FullName);
            Assert.Equal("System.String", parameters[1].ParameterType.FullName);
            Assert.Equal("searchType", parameters[0].Name);
            Assert.Equal("searchTerm", parameters[1].Name);
        }

        // Test that Results method includes appropriate local variables
        [Fact]
        public void TestResultsUsesAppropriateLocalVariables()
        {
            MethodBody methodBody = typeof(SearchController).GetMethod("Results").GetMethodBody();
            var locals = methodBody.LocalVariables;
            Assert.True(locals.Count > 1);
        }
    }
}
