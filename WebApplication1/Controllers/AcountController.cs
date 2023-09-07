using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Model.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManger;
        public AcountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManger = roleManger;

        }


        public IActionResult SignIn(UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid == false)
            {
                return NotFound(userLoginDTO);
            }

            var user = _userManager.FindByEmailAsync(userLoginDTO.Email).Result;

            _signInManager.SignOutAsync();

            var reult = _signInManager.PasswordSignInAsync(user, userLoginDTO.PassWord, true, true).Result;

            if (!reult.Succeeded)
            {
                return NotFound(reult);
            }

            return Ok(reult);
        }

        public async Task<IActionResult> RegasterAsync(UserRegasterDTO userRegasterDTO)
        {
            string Role = "Public";

            if (ModelState.IsValid == false || userRegasterDTO.PassWord != userRegasterDTO.ConfirmPassWord)
            {
                return NotFound(userRegasterDTO);
            }


            User newUser = new User()
            {
                Email = userRegasterDTO.Email,
                UserName = userRegasterDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, userRegasterDTO.PassWord);

            var roleReasult = await _roleManger.RoleExistsAsync(Role);

            if (result.Succeeded)
            {
                if (roleReasult)
                {
                    await _userManager.AddToRoleAsync(newUser, Role);
                }
                else
                {
                    var newRole = new Role
                    {
                        Name = "Public"
                    };

                    var roleResult = await _roleManger.CreateAsync(newRole);
                    await _userManager.AddToRoleAsync(newUser, newRole.Name);
                }
            }

            if (!result.Succeeded)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync();

            return Ok();
        }
    }
}
