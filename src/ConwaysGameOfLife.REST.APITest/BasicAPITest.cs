using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace ConwaysGameOfLife.REST.APITest
{
    [TestClass]
    public class BasicAPITest
    {
        // Test without JSON serialization etc.

        public HttpClient _httpClient = null!;
        public WebApplicationFactory<ConwaysGameOfLife.Server.Program>? _application;

        [TestInitialize]
        public void TestInitialize()
        {
            _application = new WebApplicationFactory<ConwaysGameOfLife.Server.Program>();
            _httpClient = _application.CreateClient();
        }

        [TestMethod]
        public async Task GetBoardMethod_ShoudlReturnOK()
        {
            var response = await _httpClient.GetAsync("api/v1/board");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetBoardMethod_ShoudlReturnOK_ResponseWithEmptyBoard()
        {
            var response = await _httpClient.GetAsync("api/v1/board");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("board"));
        }
    }
}