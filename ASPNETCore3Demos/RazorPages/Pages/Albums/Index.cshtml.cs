using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Models;

namespace RazorPages.Pages.Albums
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Models.ChinookContext _context;

        public IndexModel(RazorPages.Models.ChinookContext context)
        {
            _context = context;
        }

        public IList<Album> Album { get;set; }

        public async Task OnGetAsync()
        {
            Album = await _context.Album
                .Include(a => a.Artist).ToListAsync();
        }
    }
}
