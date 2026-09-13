using System.Text;
using System.Text.Json;

namespace AppointmentBookingApp.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeminiService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GenerateSummaryAsync(string reasonForVisit)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            var model = _configuration["Gemini:Model"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("Gemini API key is not configured.");
            }

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent";

            var prompt = $"""
                Summarize the following patient's reason for visit
                in one short, professional sentence.

                Do not diagnose the patient.
                Do not recommend treatment.
                Only summarize the information provided.

                Reason for visit:
                {reasonForVisit}
                """;

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                url);

            request.Headers.Add("x-goog-api-key", apiKey);

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);

            var responseContent =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Gemini API error: {response.StatusCode} - {responseContent}");
            }

            using var document =
                JsonDocument.Parse(responseContent);

            var summary =
                document.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

            return summary?.Trim()
                   ?? "Unable to generate summary.";
        }
    }
}