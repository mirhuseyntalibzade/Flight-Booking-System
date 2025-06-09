using DAL.Migrations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeepSeekController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string DeepSeekApiUrl = "http://localhost:11434/v1/completions";

        public DeepSeekController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateText([FromBody] DeepSeekRequest request)
        {
            var json = JsonConvert.SerializeObject(new
            {
                model = "deepseek-r1:1.5b",
                prompt = request.Prompt,
                max_tokens = request.MaxTokens
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(DeepSeekApiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to communicate with DeepSeek AI.");
            }

            var result = await response.Content.ReadAsStringAsync();

            var deepSeekResponse = JsonConvert.DeserializeObject<DeepSeekResponse>(result);

            if (deepSeekResponse == null || deepSeekResponse.Choices == null || deepSeekResponse.Choices.Count == 0)
            {
                return BadRequest("No response from DeepSeek AI.");
            }
            string pattern = @"<think>\n\n</think>\n\n";

            return Ok(new { text = Regex.Replace(deepSeekResponse.Choices[0].Text, pattern, string.Empty) });



        }

        public class DeepSeekRequest
        {
            public string Prompt { get; set; }
            public int MaxTokens { get; set; } = 50;
        }

        public class DeepSeekResponse
        {
            public List<Choice> Choices { get; set; }

            public class Choice
            {
                public string Text { get; set; }
            }
        }
    }
}
