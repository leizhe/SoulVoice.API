using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV.API.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [Route("api/Test/QueryTest")]
        public void QueryTest()
        {
            _testService.QueryTest();
        }


        [HttpGet]
        [Route("api/Test/CommandTest")]
        public void CommandTest()
        {
            _testService.CommandTest();
        }

        

    }
}
