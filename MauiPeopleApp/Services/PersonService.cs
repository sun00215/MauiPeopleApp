using System.Net.Http.Json;
using MauiPeopleApp.Models;

namespace MauiPeopleApp.Services;

public class PersonService
{
    private readonly HttpClient _httpClient;

    public PersonService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Person>> GetPeopleAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse>("https://reqres.in/api/users");
            return response?.Data ?? new List<Person>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching people: {ex.Message}");
            return new List<Person>();
        }
    }

    private class ApiResponse
    {
        public List<Person> Data { get; set; } = new List<Person>();
    }
}