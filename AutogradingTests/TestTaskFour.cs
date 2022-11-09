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
    public class TestTaskFour
    {
        public TestTaskFour()
        {
        }

        

        // Test that Results method exists and returns a view.
        [Fact]
        public void TestResultsExistsAndLoadsView()
        {
            SearchController _controller = new SearchController();
            var result = _controller.Results("coreCompetency", "Ruby");
            Assert.IsType<ViewResult>(result);
        }

        // Test that Results method returns the appropriate number of results for different searches
        [Theory]
        [InlineData("location", "new york", 1)]
        [InlineData("location", "chicago", 0)]
        [InlineData("employer", "equifax", 1)]
        [InlineData("all", "ruby", 4)]
        [InlineData("coreCompetency", "Ruby", 3)]
        public void TestResultsReturnsCorrectNumber(string searchType, string searchTerm, int expected)
        {
            SearchController _controller = new SearchController();
            var result = _controller.Results(searchType, searchTerm) as ViewResult;
            var jobs = (List<Job>)result.ViewData["jobs"];
            Assert.Equal(expected, jobs.Count);
        }

        
    }
}
