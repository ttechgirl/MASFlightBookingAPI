using MASFlightBookingAPI.Models;
using MASFlightBookingAPI.View_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MASFlightBookingAPI.Controllers
{
    //[Route("api/[controller]")]
   // [ApiController]
    public class AuthenticationController : ControllerBase
    {
        //An empty MVC controller

        //Controller atrribute

            public UserManager<Users> usermanager;
            public RoleManager<IdentityRole> rolemanager;
            private readonly ILogger<AuthenticationController> logger;
            //makes it easy to access appsettings
            public readonly IConfiguration configuration;


            //constructor
            public AuthenticationController(UserManager<Users> usermanager, RoleManager<IdentityRole> rolemanager, IConfiguration configuration, ILogger<AuthenticationController> logger)
            {
                this.usermanager = usermanager;
                this.rolemanager = rolemanager;
                this.configuration = configuration;
                this.logger = logger;

            }
            [HttpPost]
            [Route("register")]

            //Frombody is aclass that implement the ibindingsourcemetadata interface(it specifies a data source for modelbinding)
            public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
            {
                if (ModelState.IsValid)
                {
                    var user = new Users()
                    {
                        UserName= registerModel.Username,
                        Username = registerModel.Username,
                        Email = registerModel.Email,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var result = await usermanager.CreateAsync(user, registerModel.Password);
                    if (!result.Succeeded)
                    {
                      /* foreach (var error in result.Errors)
                      {
                       ModelState.AddModelError("User creation unsuccessful,please check details", error.Description);


                      }*/
                       return BadRequest(new ResponseModel { Success = false, Error = result.Errors.ToString()});
                    }
                    else
                    {
                        if (!await rolemanager.RoleExistsAsync(RoleNames.User))
                            await rolemanager.CreateAsync(new IdentityRole(RoleNames.User));
                        await usermanager.AddToRoleAsync(user, RoleNames.User);

                        var token = await usermanager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token = token }, Request.Scheme);

                        logger.Log(LogLevel.Warning, confirmationLink);

                        return Ok(new ResponseModel { Success = true, Error = "User successfully created" });
                    }
                }
                return BadRequest(new ResponseModel { Success = false, Error = "User details is invalid" });

            }

            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
            {
                var user = await usermanager.FindByNameAsync(loginModel.Username);
                if (user != null && await usermanager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var userRoles = await usermanager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));


                    var token = new JwtSecurityToken(
                        issuer: configuration["JWT:Issuer"],
                        audience: configuration["JWT.Audience"],
                        expires: DateTime.Now.AddMinutes(20),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo

                    });
                }
                return Unauthorized();

            }
            [HttpPost("register-admin")]
            public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
            {
                //checking if user exist in the admin role

                var userExists = await usermanager.FindByNameAsync(registerModel.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Success = false, Error = "Admin with the same name exist" });
                var adminUser = new Users()
                {
                    Username = registerModel.Username,
                    Email = registerModel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await usermanager.CreateAsync(adminUser, registerModel.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Success = false, Error = "Registration not successful" });

                }
                if (!await rolemanager.RoleExistsAsync(RoleNames.Admin))
                    await rolemanager.CreateAsync(new IdentityRole(RoleNames.Admin));

                if (await rolemanager.RoleExistsAsync(RoleNames.Admin))
                {
                    await usermanager.AddToRoleAsync(adminUser, RoleNames.Admin);

                }
                return Ok(new ResponseModel { Success = true, Error = "Registration Successful" });

            }

    }

    
}
