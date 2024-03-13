using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConcertLab.Web.Data;
using ConcertLab.Web.Models;

namespace ConcertLab.Web.Controllers
{
    public class ConcertTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConcertTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConcertTickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets.Include(c => c.Concert);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConcertTickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertTicket = await _context.Tickets
                .Include(c => c.Concert)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concertTicket == null)
            {
                return NotFound();
            }

            return View(concertTicket);
        }

        // GET: ConcertTickets/Create
        public IActionResult Create()
        {
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id");
            return View();
        }

        // POST: ConcertTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberOfPeople,ConcertId")] ConcertTicket concertTicket)
        {
            if (ModelState.IsValid)
            {
                concertTicket.Id = Guid.NewGuid();
                _context.Add(concertTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", concertTicket.ConcertId);
            return View(concertTicket);
        }

        // GET: ConcertTickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertTicket = await _context.Tickets.FindAsync(id);
            if (concertTicket == null)
            {
                return NotFound();
            }
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", concertTicket.ConcertId);
            return View(concertTicket);
        }

        // POST: ConcertTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NumberOfPeople,UserId,ConcertId")] ConcertTicket concertTicket)
        {
            if (id != concertTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concertTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertTicketExists(concertTicket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", concertTicket.ConcertId);
            return View(concertTicket);
        }

        // GET: ConcertTickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertTicket = await _context.Tickets
                .Include(c => c.Concert)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concertTicket == null)
            {
                return NotFound();
            }

            return View(concertTicket);
        }

        // POST: ConcertTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var concertTicket = await _context.Tickets.FindAsync(id);
            if (concertTicket != null)
            {
                _context.Tickets.Remove(concertTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertTicketExists(Guid id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
