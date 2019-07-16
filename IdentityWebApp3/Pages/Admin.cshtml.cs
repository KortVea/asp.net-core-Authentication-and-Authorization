using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdentityWebApp3.Data;
using IdentityWebApp3.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityWebApp3.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IdentityWebApp3.Data.ApplicationDbContext _context;

        public AdminModel(IdentityWebApp3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<IdentityUser> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            Users = await _context.Users.ToListAsync();//.ContinueWith(i => i.);

            //if (User == null)
            //{
            //    return NotFound();
            //}
            return Page();
        }
    }
}
