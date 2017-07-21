using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GenesController : Controller
    {
        private readonly GeneAnnotationDBContext _context;

        public GenesController(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        // GET: api/Genes
        [HttpGet]
        public IEnumerable<Gene> GetGene()
        {
            return _context.Gene;
        }

        // GET: api/Genes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGene([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gene = await _context.Gene.SingleOrDefaultAsync(m => m.Id == id);

            if (gene == null)
            {
                return NotFound();
            }

            return Ok(gene);
        }

        // PUT: api/Genes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGene([FromRoute] int id, [FromBody] Gene gene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gene.Id)
            {
                return BadRequest();
            }

            _context.Entry(gene).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneExists(id))
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

        // POST: api/Genes
        [HttpPost]
        public async Task<IActionResult> PostGene([FromBody] Gene gene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gene.Add(gene);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGene", new { id = gene.Id }, gene);
        }

        // DELETE: api/Genes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGene([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gene = await _context.Gene.SingleOrDefaultAsync(m => m.Id == id);
            if (gene == null)
            {
                return NotFound();
            }

            _context.Gene.Remove(gene);
            await _context.SaveChangesAsync();

            return Ok(gene);
        }

        private bool GeneExists(int id)
        {
            return _context.Gene.Any(e => e.Id == id);
        }
    }
}