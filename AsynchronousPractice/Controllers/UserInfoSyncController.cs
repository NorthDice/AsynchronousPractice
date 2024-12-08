using Microsoft.AspNetCore.Mvc;

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
          var userJson = System.IO.File.ReadAllText(USERS_FILE_PATH);
          Thread.Sleep(1000);
          
        }

        private string GetRandomUserIdAsync()
        {

        }
        private string GetUserLocationAsync(int userId)
        {

        }

        private string GetUserFavoriteGameAsync(int userId)
        {

        }
    }
}
