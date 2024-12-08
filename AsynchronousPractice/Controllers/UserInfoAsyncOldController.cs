//using AsynchronousPractice.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Threading;

//namespace AsynchronousPractice.Controllers
//{
//    public class UserInfoAsyncOldController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//        private const string USERS_FILE_PATH = "Data/users.json";
//        private const string LOCATIONS_FILE_PATH = "Data/location.json";
//        private const string GAMES_FILE_PATH = "Data/games.json";


//        [HttpGet]
//        [Route("GetUserInfo")]
//        public Task<ActionResult> GetUserInfo()
//        {
           
//            var userId = GetRandomUserId();

//            userId.ContinueWith(user =>
//            {
//                var location = GetUserLocation(user.Result);
//                var favoriteGame = GetUserFavoriteGame(user.Result);
//            });
                


//            return Ok(new { userId, location, favoriteGame });

//        }

//        private Task<int> GetRandomUserId()
//        {
//            Debug.WriteLine("Reading users file");

//            var result = Task.Run(() =>
//            {
//                var userJsonTask = System.IO.File.ReadAllTextAsync(USERS_FILE_PATH);
//                Task.Delay(3000).Wait();

//                return userJsonTask;
//            });

//            //Free thread

//            result.ContinueWith(resultTask =>
//            {
//                var userData = JsonSerializer.Deserialize<UserData>(resultTask.Result)
//                ?? throw new NullReferenceException("Users json is empty!");

//                Debug.WriteLine("Users file readed successfully");

//                return userData.Users.First().Id;
//            });
            

            

            

//            return Task.FromResult(userData.Users.First().Id);
//        }

//        private Task GetUserLocation(int userId)
//        {
//            Debug.WriteLine("Reading location file");
//            var locationJsonTask = System.IO.File.ReadAllTextAsync(LOCATIONS_FILE_PATH);




//            Task.Delay(2000).Wait();

//            Debug.WriteLine("Location file readed successfully");

//            var locationData = JsonSerializer.Deserialize<LocationData>(locationJson)
//                ?? throw new NullReferenceException("Location json is empty!");

//            return locationData.Locations.First(l => l.UserId == userId).LocationName;

//        }

//        private Task GetUserFavoriteGame(int userId)
//        {
//            Debug.WriteLine("Reading games file");
//            var gameJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
//            Task.Delay(3000).Wait();

//            Debug.WriteLine("Games file readed successfully");

//            var gameData = JsonSerializer.Deserialize<GameData>(gameJson)
//                ?? throw new NullReferenceException("Game json is empty!");

//            return gameData.Games.First(g => g.UserId == userId).FavoriteGame;
//        }
//    }
//}
