﻿using AsynchronousPractice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AsynchronousPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoSyncController : Controller
    {
        private const string USERS_FILE_PATH = "Data/users.json";
        private const string LOCATIONS_FILE_PATH = "Data/location.json";
        private const string GAMES_FILE_PATH = "Data/games.json";


        [HttpGet]
        [Route("GetUserInfo")]
        public ActionResult GetUserInfo()
        {
            try
            {
                var userId = GetRandomUserId();
                var location = GetUserLocation(userId);
                var favoriteGame = GetUserFavoriteGame(userId);

                return Ok(new { userId, location, favoriteGame });
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine($"Error occurred: {ex.Message}");

                return StatusCode(500, new
                {
                    Error = "An error occurred while fetching user info",
                    Details = ex.Message
                });
            }

        }

        private int GetRandomUserId()
        {
            Debug.WriteLine("Reading users file");
            var userJson = System.IO.File.ReadAllText(USERS_FILE_PATH);
            Task.Delay(1000).Wait();

            Debug.WriteLine("Users file readed successfully");

            var userData = JsonSerializer.Deserialize<UserData>(userJson) 
                ?? throw new NullReferenceException("Users json is empty!");

            return userData.Users.First().Id;
        }

        private string GetUserLocation(int userId)
        {
            Debug.WriteLine("Reading location file");
            var locationJson = System.IO.File.ReadAllText(LOCATIONS_FILE_PATH);
            Task.Delay(2000).Wait();

            Debug.WriteLine("Location file readed successfully");

            var locationData = JsonSerializer.Deserialize<LocationData>(locationJson)
                ?? throw new NullReferenceException("Location json is empty!");

            return locationData.Locations.First(l => l.UserId == userId).LocationName;

        }

        private string GetUserFavoriteGame(int userId)
        {
            Debug.WriteLine("Reading games file");
            var gameJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
            Task.Delay(3000).Wait();

            Debug.WriteLine("Games file readed successfully");

            var gameData = JsonSerializer.Deserialize<GameData>(gameJson)
                ?? throw new NullReferenceException("Game json is empty!");

            return gameData.Games.First(g=>g.UserId == userId).FavoriteGame;
        }
    }
}
