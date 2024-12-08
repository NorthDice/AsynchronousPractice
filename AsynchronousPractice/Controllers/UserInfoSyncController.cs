using AsynchronousPractice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AsynchronousPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoSyncController : ControllerBase
    {
        private const string USERS_FILE_PATH = "Data/users.json";
        private const string LOCATIONS_FILE_PATH = "Data/location.json";
        private const string GAMES_FILE_PATH = "Data/games.json";

        [HttpGet]
        public ActionResult GetUserInfo()
        {
            var userId = GetRandomUserId();

            var location = GetUserLocation(userId);

            var favoriteGame = GetUserFavoriteGame(userId);

            return Ok(new { userId, location, favoriteGame });

        }

        private int GetRandomUserId()
        {
            var userJson = System.IO.File.ReadAllText(USERS_FILE_PATH);
            Thread.Sleep(1000);

            var userData = JsonSerializer.Deserialize<UserData>(userJson) 
                ?? throw new NullReferenceException("Users json is empty!");

            return userData.Users.First().Id;
        }

        private string GetUserLocation(int userId)
        {
            var locationJson = System.IO.File.ReadAllText(LOCATIONS_FILE_PATH);
            Thread.Sleep(3000);

            var locationData = JsonSerializer.Deserialize<LocationData>(locationJson)
                ?? throw new NullReferenceException("Location json is empty!");

            return locationData.Locations.First(l => l.UserId == userId).LocationName;

        }

        private string GetUserFavoriteGame(int userId)
        {
            var gameJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
            Thread.Sleep(3000);

            var gameData = JsonSerializer.Deserialize<GameData>(gameJson)
                ?? throw new NullReferenceException("Game json is empty!");

            return gameData.Games.First(g=>g.UserId == userId).FavoriteGame;
        }
    }
}
