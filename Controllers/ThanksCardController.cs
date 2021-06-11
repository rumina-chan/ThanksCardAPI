using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardAPI.Models;

namespace ThanksCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanksCardController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ThanksCardController(ApplicationContext context)
        {
            _context = context;
        }

        #region GetThanksCards
        // GET: api/ThanksCard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThanksCard>>> GetThanksCards()
        {
            // Include を指定することで From, To (Userモデル) を同時に取得する。
            return await _context.ThanksCards
                                    .Include(ThanksCard => ThanksCard.From)
                                    .Include(ThanksCard => ThanksCard.To)
                                    .Include(ThanksCard => ThanksCard.ThanksCardTags)
                                        .ThenInclude(ThanksCardTag => ThanksCardTag.Tag)
                                    .ToListAsync();
        }
        #endregion

        // GET: api/ThanksCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThanksCard>> GetThanksCard(long id)
        {
            var thanksCard = await _context.ThanksCards.FindAsync(id);

            if (thanksCard == null)
            {
                return NotFound();
            }

            return thanksCard;
        }

        // POST api/ThanksCard
        [HttpPost]
        public async Task<ActionResult<ThanksCard>> Post([FromBody] ThanksCard thanksCard)
        {
            // From, To には既に存在しているユーザが入るため、更新の対象から外す。
            //_context.Users.Attach(thanksCard.From);
            //_context.Users.Attach(thanksCard.To);

            _context.ThanksCards.Add(thanksCard);
            await _context.SaveChangesAsync();
            // TODO: Error Handling
            return thanksCard;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThanksCard(long id, ThanksCard thanksCard)
        {
            if (id != thanksCard.Id)
            {
                return BadRequest();
            }

            // Department には既に存在しているユーザが入るため、更新の対象から外す。
            //_context.Departments.Attach(user.Department);

            _context.Entry(thanksCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThanksCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ThanksCard>> DeleteThanksCard(long id)
        {
            var thanksCard = await _context.ThanksCards.FindAsync(id);
            if (thanksCard == null)
            {
                return NotFound();
            }

            _context.ThanksCards.Remove(thanksCard);
            await _context.SaveChangesAsync();

            return thanksCard;
        }

        private bool ThanksCardExists(long id)
        {
            return _context.ThanksCards.Any(e => e.Id == id);
        }
    }       
}
