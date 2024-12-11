using AsynchronousPractice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AsynchronousPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoAsyncController : Controller
    {
        private const string USERS_FILE_PATH = "Data/users.json";
        private const string LOCATIONS_FILE_PATH = "Data/location.json";
        private const string GAMES_FILE_PATH = "Data/games.json";


        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<ActionResult> GetUserInfo()
        {
           
            var userId = await GetRandomUserId();
            var location =  GetUserLocation(userId);
            var favoriteGame =  GetUserFavoriteGame(userId);

            var result = await Task.WhenAll(location, favoriteGame);

            var first = result[0];
            var second = result[1];

            return Ok(new { userId, first, second });
            
        }

        private async Task<int> GetRandomUserId()
        {
            Debug.WriteLine("Reading users file");
            var userJson = await System.IO.File.ReadAllTextAsync(USERS_FILE_PATH);
            await Task.Delay(3000);

            Debug.WriteLine("Users file readed successfully");

            var userData = JsonSerializer.Deserialize<UserData>(userJson)
                ?? throw new NullReferenceException("Users json is empty!");

            return userData.Users.First().Id;
        }

        private async Task<string> GetUserLocation(int userId)
        {
            Debug.WriteLine("Reading location file");
            var locationJson = await System.IO.File.ReadAllTextAsync(LOCATIONS_FILE_PATH);
            await Task.Delay(2000);

            Debug.WriteLine("Location file readed successfully");

            var locationData = JsonSerializer.Deserialize<LocationData>(locationJson)
                ?? throw new NullReferenceException("Location json is empty!");

            return locationData.Locations.First(l => l.UserId == userId).LocationName;

        }

        private async Task<string> GetUserFavoriteGame(int userId)
        {
            Debug.WriteLine("Reading games file");
            var gameJson = await System.IO.File.ReadAllTextAsync(GAMES_FILE_PATH);
            await Task.Delay(3000);

            Debug.WriteLine("Games file readed successfully");

            var gameData = JsonSerializer.Deserialize<GameData>(gameJson)
                ?? throw new NullReferenceException("Game json is empty!");

            return gameData.Games.First(g => g.UserId == userId).FavoriteGame;
        }
    }
}
