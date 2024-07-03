// FCCopenhagen.Client/Services/PlayerService.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FCCopenhagen.Shared;
using FCCopenhagen.Data;


namespace FCCopenhagen.Client.Services
{
    public class PlayerService
    {
        private readonly HttpClient _httpClient;

        public PlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            var url = "https://localhost:7099/api/Proxy/players"; // Call to the backend proxy endpoint

            try
            {
                Console.WriteLine($"Fetching data from: {url}");

                var responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();

                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine("Client received response:");
                Console.WriteLine(responseContent); // Log the response content

                if (responseContent.StartsWith("<"))
                {
                    throw new Exception("Received HTML instead of JSON. Possible server-side error.");
                }

                var response = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(responseContent, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (response != null && response.Data != null)
                {
                    Console.WriteLine($"Deserialized {response.Data.Count} players.");
                }
                else
                {
                    Console.WriteLine("Failed to deserialize response or response is empty.");
                }

                var players = new List<Player>();

                foreach (var squad in response.Data ?? new List<SquadData>())
                {
                    Console.WriteLine($"Name: {squad.Player?.Name}, ImagePath: {squad.Player?.Image_Path}");
                    players.Add(new Player
                    {
                        Id = squad.Player?.Id ?? 0,
                        Name = squad.Player?.Name,
                        Nationality = squad.Position?.Name,
                        Age = CalculateAge(squad.Player?.Date_Of_Birth),
                        Position = squad.Position?.Name,
                        DetailedPosition = squad.DetailedPosition?.Name,
                        Image_Path = squad.Player?.Image_Path
                    });
                }

                return players;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching players: {ex.Message}");
                return new List<Player>();
            }
        }

        private int CalculateAge(string? date_Of_Birth)
        {
            if (string.IsNullOrEmpty(date_Of_Birth))
                return 0;

            var dob = DateTime.Parse(date_Of_Birth);
            var age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age -= 1;

            return age;
        }
    }
}