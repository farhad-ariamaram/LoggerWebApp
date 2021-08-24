using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggerWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoggerWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogDBContext _context;

        public IndexModel(ILogDBContext context)
        {
            _context = context;
        }

        public int IpsNumber { get; set; }
        public int SIpsNumber { get; set; }
        public int IIpsNumber { get; set; }
        public int ErrorsNumber { get; set; }
        public int SErrorsNumber { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var ips = await _context.TblIlogDbIps.ToListAsync();
            IpsNumber = ips.Count();
            SIpsNumber = ips.Where(a => a.FldIlogDbLogTypeId == 1).Count();
            IIpsNumber = ips.Where(a => a.FldIlogDbLogTypeId == 2).Count();
            ErrorsNumber = await _context.TblIlogDbLogs.CountAsync();
            SErrorsNumber = await _context.TblIlogDbLogs.Include(t=>t.FldIlogDbIp).Where(a => a.FldIlogDbIp.FldIlogDbLogTypeId == 1).CountAsync();

            return Page();
        }
    }
}
