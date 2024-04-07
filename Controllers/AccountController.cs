using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksApp.Dtos.Account;
using StocksApp.Dtos.Email;
using StocksApp.Interfaces;
using StocksApp.Models;

namespace StocksApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);   //validates model state and returns an error based on user input
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var email = new EmailDto
                {
                    To = registerDto.Email,
                    Subject = "Registration Sucessful",
                    UserName = registerDto.Username,
                    OTP = "1234"
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password); //creates the user and stores the hashed password 
                if (createUser.Succeeded)
                {
                    var roleUp = await _userManager.AddToRoleAsync(appUser, "User"); //assigns new user to a role(user)

                    if (roleUp.Succeeded)
                    {
                        await _emailService.SendEmailRegistration(email);
                        return Ok
                        (


                            new NewUserDto //returns user info back to user
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    } //confirms that user has been created with a valid role
                      
                    else return StatusCode(500, roleUp.Errors);
                }
                else return StatusCode(500, createUser.Errors);

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName); //compares the username with existing username
            if (user == null) return Unauthorized("Invalid");

            var pass = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!pass.Succeeded) return Unauthorized("Username/password incorrect");

            return Ok
            (
                new NewUserDto //returns user info back to user
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}
