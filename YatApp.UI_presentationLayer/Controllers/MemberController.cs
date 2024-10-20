using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ApiConsume;
using Library.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dto;
using YatApp.UI_presentationLayer.Base;
using Interface;
using Humanizer;

namespace Library.Controllers
{
    public class MemberController : BaseUiController
    {
        public MemberController(IApiCall api) : base(api)
        {
        }


        #region IndexForHost
        public async Task<IActionResult> IndexForHost(int memberId)
        {
            var members = await _api.GetByRoleAsync<Member>("members/GetByRoleAsync", 1);
            return View("IndexForHost", members);  
        }
        #endregion

        #region IndexForMember
        public async Task<IActionResult> IndexForMember(int memberId)
        {
            var members = await _api.GetByIdAsync<Member>("members/GetByIdAsync",memberId);
            if(members == null)
            {
                return NotFound("Member Not Found.");
            }
            return RedirectToAction("IndexForMember");
        } 
        #endregion

        #region Details
        public async Task<ActionResult> Details(int id)
        {
            var member = await _api.GetByIdAsync<Member>("members", id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        #endregion

        #region Create
        public async Task<ActionResult> Create()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            if (!await IsUserHost())
            {
                return View("AccessDenied");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Member member)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            if (!await IsUserHost())
            {
                return View("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                var hostRole = await _api.GetAllAsync<Role>("roles");
                var hostRoleObj = hostRole.FirstOrDefault(r => r.RoleName == "Host");

                if (hostRoleObj != null)
                {
                    member.RoleId = hostRoleObj.RoleId;
                }

                await _api.CreateAsync("members", member);
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        private bool IsUserLoggedIn()
        {
            var memberId = HttpContext.Session.GetString("MemberId");
            return !string.IsNullOrEmpty(memberId);
        }

        private async Task<bool> IsUserHost()
        {
            var memberId = int.Parse(HttpContext.Session.GetString("MemberId"));
            var currentMember = await _api.GetByIdAsync<Member>("members", memberId);

            return currentMember != null && currentMember.Role.RoleName == "Host";
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            var member = await _api.GetByIdAsync<Member>("members", id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _api.UpdateAsync("members", id, member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(int id)
        {
            var member = await _api.GetByIdAsync<Member>("members", id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var member = await _api.GetByIdAsync<Member>("members", id);
            if (member != null)
            {
                await _api.DeleteAsync<Member>("members", id);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberDto dto)
        {
            // Validate the input
            if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var member = await _api.GetMemberByUserameAsync<MemberDto>("Member/GetMemberByUsernameAsync", dto.Username);

            if (member == null)
            {
                return NotFound("Member not found");
            }


            if (VerifyPassword(dto.Password, member.Password)) 
            {
                
                if (member.RoleId == 1)
                {
                    return RedirectToAction("IndexForHost");
                }
                else if (member.RoleId == 2)
                {
                    return RedirectToAction("IndexForMember");
                }
            }

            return View("AccessDenied");
        }

        
        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
           
            return inputPassword == storedPassword; 
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberDto dto)
        {
            if (ModelState.IsValid)
            {
                dto.RoleId = 2;

                await _api.CreateAsync("members", dto);

                HttpContext.Session.SetString("Username", dto.MemberName);

                return RedirectToAction("Index","Home");
            }

            return RedirectToAction("IndexForMember");
        }
        #endregion
    }
}
