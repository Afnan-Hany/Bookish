using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;  
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace Library.Controllers
{
    public class MemberController : Controller
    {
        private readonly BookishAppDbContext _context;

        public MemberController(BookishAppDbContext context)
        {
            _context = context;
        }

        //public IActionResult SomeAction()
        //{
        //    var currentUserRole = HttpContext.Session.GetString("UserRole");
        //    var currentUserId = HttpContext.Session.GetInt32("MemberId");

        //    ViewBag.UserRole = currentUserRole;
        //    ViewBag.MemberId = currentUserId;

        //    return View();
        //}

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



        //#region Index
        //// This action will determine the role and load the appropriate view
        //public IActionResult Index(int memberId)
        //{
        //    // Get the current user from the database
        //    var member = _context.Members
        //        .Include(m => m.Role)  // Include the Role data
        //        .FirstOrDefault(m => m.MemberId == memberId);

        //    if (member == null)
        //    {
        //        return NotFound();  // If no member found
        //    }

        //    // Check the role
        //    if (member.Role.RoleName == "Host")
        //    {
        //        // Get all members if the current user is Host
        //        var members = _context.Members.Include(m => m.Role).ToList();
        //        return View("Index", members);  // Load all members for Host
        //    }
        //    else if (member.Role.RoleName == "Member")
        //    {
        //        return View("Index", member);  // Load Member view
        //    }

        //    return View("AccessDenied");  // Default access denied view
        //}
        //#endregion

        [Authorize]
        #region Index
        // This action will determine the role and load the appropriate view
        public IActionResult Index(int memberId)
        {
            // Get the current user from the database
            var member = _context.Members
                .Include(m => m.Role)  // Include the Role data
                .FirstOrDefault(m => m.MemberId == memberId);

            if (member == null)
            {
                return NotFound();  // If no member found
            }

            // Check the role
            if (member.Role.RoleName == "Host")
            {
                // Get all members if the current user is Host
                var members = _context.Members.Include(m => m.Role).ToList();
                return View("Index", members);  // Pass the list of members to the view
            }
            else if (member.Role.RoleName == "Member")
            {
                // Wrap the single member in a list and pass it to the view
                var singleMemberList = new List<Member> { member };
                return View("Index", singleMemberList);  // Pass as IEnumerable<Member>
            }

            return View("AccessDenied");  // Default access denied view
        }
        #endregion



        #region Details
        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            var member = _context.Members
                .Include(m => m.Role)
                .FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        #endregion


        #region Create
        // GET: MemberController/Create (Host-only view)
        public ActionResult Create()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            // Check if the logged-in user is a Host
            if (!IsUserHost())
            {
                return View("AccessDenied");
            }

            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            // Check if the logged-in user is a Host
            if (!IsUserHost())
            {
                return View("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                // Assign role as Host
                var hostRole = _context.Roles.FirstOrDefault(r => r.RoleName == "Host");
                if (hostRole != null)
                {
                    member.RoleId = hostRole.RoleId;
                }

                _context.Members.Add(member);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // If model state is not valid, return the same view with validation messages
            return View(member);
        }

        // Helper method to check if the user is logged in
        private bool IsUserLoggedIn()
        {
            var memberId = HttpContext.Session.GetString("MemberId");
            return !string.IsNullOrEmpty(memberId);
        }

        // Helper method to check if the current user is a Host
        private bool IsUserHost()
        {
            var memberId = int.Parse(HttpContext.Session.GetString("MemberId"));
            var currentMember = _context.Members.Include(m => m.Role).FirstOrDefault(m => m.MemberId == memberId);

            return currentMember != null && currentMember.Role.RoleName == "Host";
        }
        #endregion


        #region Edit

        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(member);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        #endregion

        #region Delete
        // GET: MemberController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var member = _context.Members.Find(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Login
        // GET: MemberController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: MemberController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var member = _context.Members
                .Include(m => m.Role)
                .FirstOrDefault(m => m.Username == username && m.Password == password);

            if (member != null)
            {
                // Set session or cookie for logged-in user
                HttpContext.Session.SetString("MemberId", member.MemberId.ToString());
                HttpContext.Session.SetString("Username", member.Username);
                return RedirectToAction(nameof(Index), new { memberId = member.MemberId });
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
        #endregion


        #region Register
        // GET: MemberController/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: MemberController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Member model)
        {
            if (ModelState.IsValid)
            {
                // تعيين دور "Member" افتراضيًا
                var memberRole = _context.Roles.FirstOrDefault(r => r.RoleName == "Member");
                if (memberRole != null)
                {
                    model.RoleId = memberRole.RoleId; // تعيين الدور كـ Member
                }

                _context.Members.Add(model);
                _context.SaveChanges();

                // تسجيل الدخول بعد التسجيل
                HttpContext.Session.SetString("MemberId", model.MemberId.ToString());
                HttpContext.Session.SetString("Username", model.MemberName);

                return RedirectToAction(nameof(Index), new { memberId = model.MemberId });
            }

            return View(model); // إذا كانت البيانات غير صحيحة، أعد عرض النموذج
        }
        #endregion

    }
}




