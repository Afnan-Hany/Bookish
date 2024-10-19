using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ApiConsume;
using Library.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dto;

namespace Library.Controllers
{
    public class MemberController : Controller
    {
        private readonly IApiCall _api;

        public MemberController(IApiCall api)
        {
            _api = api;
        }

        public IActionResult SomeAction()
        {
            var currentUserRole = HttpContext.Session.GetString("UserRole");
            var currentUserId = HttpContext.Session.GetInt32("MemberId");

            if (currentUserRole == null || currentUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserRole = currentUserRole;
            ViewBag.MemberId = currentUserId;

            return View();
        }

        //[Authorize]
        #region Index
        public async Task<IActionResult> Index(int memberId)
        {
            var member = await _api.GetByIdAsync<Member>("members", memberId);
            if (member == null)
            {
                return NotFound();  // If no member found
            }

            if (member.Role.RoleName == "Host")
            {
                var members = await _api.GetAllAsync<Member>("members");
                return View("Index", members);  // Pass the list of members to the view
            }
            else if (member.Role.RoleName == "Member")
            {
                var singleMemberList = new List<Member> { member };
                return View("Index", singleMemberList);  // Pass as IEnumerable<Member>
            }

            return View("AccessDenied");
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
        public async Task<ActionResult> Login(string username, string password)
        {
            var members = await _api.GetAllAsync<Member>("members");
            var member = members.FirstOrDefault(m => m.Username == username && m.Password == password);

            if (member != null)
            {
                HttpContext.Session.SetString("MemberId", member.MemberId.ToString());
                HttpContext.Session.SetString("Username", member.Username);
                return RedirectToAction(nameof(Index), new { memberId = member.MemberId });
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Member model)
        {
            if (ModelState.IsValid)
            {
                var memberRole = await _api.GetAllAsync<Role>("roles");
                var memberRoleObj = memberRole.FirstOrDefault(r => r.RoleName == "Member");
                if (memberRoleObj != null)
                {
                    model.RoleId = memberRoleObj.RoleId;
                }

                await _api.CreateAsync("members", model);

                HttpContext.Session.SetString("MemberId", model.MemberId.ToString());
                HttpContext.Session.SetString("Username", model.MemberName);

                return RedirectToAction(nameof(Index), new { memberId = model.MemberId });
            }

            return View(model);
        }
        #endregion
    }
}
