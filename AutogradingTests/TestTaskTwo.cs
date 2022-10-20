using System;
using Xunit;
using Moq;
using TechJobsMVCAutograded.Controllers;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace AutogradingTests
{
    public class TestTaskTwo
    {
        public TestTaskTwo()
        {
        }

        // Testing that the Index view loads when called from ListController.
        [Fact]
        public void TestListIndexLoads()
        {
            ListController _controller = new ListController();
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        // Testing that the Jobs view loads when called from ListController.
        [Fact]
        public void TestJobListingLoads()
        {
            ListController _controller = new ListController();
            var result = _controller.Jobs("coreCompetency", "Ruby");
            Assert.IsType<ViewResult>(result);
        }

        // Testing that the job listing has all of the appropriate fields and the correct number of jobs.
        [Fact]
        public void TestJobListingDisplaysAllJobFields()
        {
            ListController _controller = new ListController();
            var result = _controller.Jobs("coreCompetency", "Ruby") as ViewResult;
            var jobs = (List<Job>)result.ViewData["jobs"];
            Assert.Equal(3, jobs.Count);
            Assert.Equal(3, jobs[0].Id);
            Assert.Equal("Junior Web Developer", jobs[0].Name.ToString());
            Assert.Equal("Cozy", jobs[0].Employer.ToString());
            Assert.Equal("Portland", jobs[0].Location.ToString());
            Assert.Equal("Web - Front End", jobs[0].PositionType.ToString());
            Assert.Equal("Ruby", jobs[0].CoreCompetency.ToString());
        }
    }
}
