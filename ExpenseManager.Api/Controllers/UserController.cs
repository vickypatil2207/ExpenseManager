using ExpenseManager.Api.Service.Interfaces;
using ExpenseManager.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]UserModel userModel)
        {
            var result = await _userService.CreateUser(userModel);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("profile/{id:int}/update")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UserModel userModel)
        {
            var result = await _userService.UpdateUser(id, userModel);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id:int}/deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var result = await _userService.DeactivateUser(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SigninModel signInModel)
        {
            var result = await _userService.SignIn(signInModel);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
