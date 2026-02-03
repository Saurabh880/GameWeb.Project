using GameUser.API.DbContextApp;
using GameUser.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GameUser.API.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class GameUserController : ControllerBase
    {
        private readonly CustomerDbContext _dbContext;

        public GameUserController(CustomerDbContext customerDb)
        {
            _dbContext = customerDb;
        }
        [HttpGet]
        public ActionResult<IEnumerable<GameUsers>> GetCustomer()
        {
            return _dbContext.CustomerModels;
        }
        [HttpGet("{cutomerId:int}")]
        public async Task<ActionResult<GameUsers>> GetCustomerById(int cutomerId)
        {
            var gameUser = await _dbContext.CustomerModels.FindAsync(cutomerId);
            if (gameUser == null)
            {
                return NotFound();
            }
            return gameUser;
        }
        [HttpPost]
        public async Task<ActionResult> CreateCustomer(GameUsers newUser)
        {
            await _dbContext.CustomerModels.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(GameUsers updateUser)
        {
            _dbContext.CustomerModels.Update(updateUser);
            await _dbContext.SaveChangesAsync();
            return Ok(); 
        }
        [HttpDelete("gameuserId:int")]
        public async Task<ActionResult> DeleteUser(int gameuserId)
        {
            var gameUser =await _dbContext.CustomerModels.FindAsync(gameuserId);
            _dbContext.CustomerModels.Remove(gameUser);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
