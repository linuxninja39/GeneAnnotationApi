using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GeneAnnotationApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LiteraturesController : Controller
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IMapper _mapper;

        public LiteraturesController(GeneAnnotationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Genes
        [HttpGet]
        public async Task<IActionResult> GetLiteratures()
        {
            if (_context.Literature == null) return BadRequest();
            var literatureEntities = _context.Literature;
            var literatureDtos = literatureEntities.ProjectTo<LiteratureDto>().ToList();
            return Ok(literatureDtos);
        }

        // GET: api/Genes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGene([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gene = await _context
                .Gene
                .Include(g => g.GeneName)
                .Include(g => g.GeneLocation)
                .Include(g => g.GeneOriginType)
                .Include(g => g.GeneVariant)
                    .ThenInclude(variant => variant.ZygosityType)
                .Include(g => g.GeneVariant)
                    .ThenInclude(variant => variant.CallType)
                .Include(g => g.GeneVariant)
                    .ThenInclude(variant => variant.VariantType)
                .Include(g => g.AnnotationGene)
                    .ThenInclude(annotationGene => annotationGene.Annotation)
                .Include(g => g.Symbol)
                .Include(g => g.Synonym)
                .Include(g => g.Chromosome)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (gene == null)
            {
                return NotFound();
            }

            var geneDto = _mapper.Map<GeneDto>(gene);
            
            return Ok(geneDto);
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