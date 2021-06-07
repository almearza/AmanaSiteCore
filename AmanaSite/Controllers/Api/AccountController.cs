using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AmanaSite.Interfaces;
using AmanaSite.Models;
using AmanaSite.Models.VM;
using AmanaSite.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AmanaSite.Controllers.Api
{
    [Authorize(policy:"AdminLevel")]
    //[AllowAnonymous]
    public class AccountController : ApiBaseController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config,
                                 UserManager<AppUser> userManager,
                                 RoleManager<AppRole> roleManager,
                                 SignInManager<AppUser> signInManager,
                                 ITokenService tokenService,
                                 IMapper mapper)
        {
            this._config = config;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserVM>> Register(RegisterVM registerVm)
        {
            if (await IsRegistered(registerVm.Username))
                return BadRequest("المستخدم مسجل مسبقا");

            var user = _mapper.Map<AppUser>(registerVm);
            var userResult = await _userManager.CreateAsync(user, _config["DefaultPassword"].ToString());
            if (!userResult.Succeeded) return BadRequest(userResult.Errors);
            
            if (!await _roleManager.RoleExistsAsync("AdminLevel"))
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = "AdminLevel"
                });
            }
            var roleResult = await _userManager.AddToRoleAsync(user, "AdminLevel");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new UserVM
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Fullname = user.FullName,
                Phonenumber = user.PhoneNumber
            };
        }
        [HttpPut("update-user")]
        public async Task<ActionResult<UserVM>> UpdateUser(UserDto userDto){
            if(string.IsNullOrEmpty(userDto.Phonenumber)||string.IsNullOrEmpty(userDto.Fullname))
            return BadRequest("الرجاء التأكد من إدخال بيانات صحيحة");
            var user =await _userManager.FindByNameAsync(userDto.Username);
            if(user==null) return NotFound("المستخدم المراد تعديله غير موجود");
            
            user.PhoneNumber=userDto.Phonenumber;
            user.FullName=userDto.Fullname;
            var result  = await _userManager.UpdateAsync(user);
            if(result.Succeeded) return NoContent();
            return BadRequest ("حدث خطأ أثناء تعديل المستخدم");
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserVM>> Login(LoginVM loginVM)
        {
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user == null) return Unauthorized("اسم المستخدم غير صالح");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, false);
            if (!result.Succeeded)
                return Unauthorized();
            return new UserVM
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Fullname = user.FullName,
                Phonenumber = user.PhoneNumber
            };
        }

        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(u => u.Role)
            .Select(u => new UserDto
            {
               Id= u.Id,
               Username  = u.UserName,
                Fullname=u.FullName,
                Phonenumber=u.PhoneNumber,
                Locked=(u.LockoutEnd != null && u.LockoutEnd > DateTime.Now),
                Roles = u.UserRoles.Select(u => u.Role.Name).ToList()
            })
            .ToListAsync();
            return Ok(users);
        }
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("لا يوجد مستخدم بهذا الاسم");
            var selectedRoles = roles.Split(',').ToArray();
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in selectedRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new AppRole { Name = role });
            }
            var addingResult = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!addingResult.Succeeded)
                return BadRequest("غير قادر على اضافة الصلاحية");
            var removingOldRoles = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!removingOldRoles.Succeeded)
                return BadRequest("خطأ أثناء إضافة الصلاحية");
            return Ok(await _userManager.GetRolesAsync(user));
        }

        [HttpPut("activate/{username}")]
        public async Task<ActionResult> Activate(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound("لا يوجد مستخدم بهذا الاسم");
            //check status:enabled or disabled
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)//locked
                user.LockoutEnd = DateTime.Now;//unlock
            else
            {
                var newDate = user.LockoutEnd = DateTime.Now.AddYears(200);
                user.LockoutEnd = newDate;
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return NoContent();

            return BadRequest("خطأ أثناء تحديث بيانات المستخدم");

        }

        [HttpPut("reset-password/{username}")]
        public async Task<ActionResult> ResetPassword(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("لا يوجد مستخدم بهذا الاسم");

            var _token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, _token, _config["DefaultPassword"]);
            if (result.Succeeded) return Ok("Done");
            return BadRequest("خطأ أثناء تحديث بيانات المستخدم");
        }

        [HttpPost("change-password/{newPassword}")]
        [Authorize(Policy = "AllLevels")]
        public async Task<ActionResult> ChangePass(string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
                return BadRequest("الرجاء إدخال كلمة السر أولا");

            var user = await _userManager.FindByNameAsync(User.GetUsername());
            if (user == null) return NotFound("لا يوجد مستخدم بهذا الاسم");
            string _token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, _token, newPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return Ok("Done");
            }
            return BadRequest("خطأ أثناء تحديث بيانات المستخدم");
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var userDto = _mapper.Map<UserDto>(user);
            // var userToReturn = _mapper.Map<MemberDto>(user);
            return Ok(userDto);
        }

        private async Task<bool> IsRegistered(string username)
        {
            var result = await _userManager.Users.AnyAsync(u => u.UserName == username);
            return result;
        }

    }
}