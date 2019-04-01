using System;
using Newtonsoft.Json;
using src;
using src.Model;
using Xunit;

namespace test
{
    public class AutoChallengeTests
    {
        RestHttpClient client = new RestHttpClient();

        [Fact]
        public async void Test_Execute()
        {
            var auto = new AutoChallenge();
            var status =  await auto.Execute();

            Assert.Equal("Congratulations.", status.Message);
            Assert.Equal("true", status.Success);
           
        }
    }
}