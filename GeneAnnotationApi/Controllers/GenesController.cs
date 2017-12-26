using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories;
using GeneAnnotationApi.Utils;

namespace GeneAnnotationApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GenesController : Controller
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IGeneRepository _geneRepository;

        private const string QStart = "start";
        private const string QEnd = "end";
        private const string QGlobalFilter = "globalFilter";
        private const string QPageStart = "pageStart";
        private const string QPageCount = "pageCount";

        public GenesController(GeneAnnotationDBContext context, IMapper mapper, IGeneRepository geneRepository,
            IGeneCoordinateRepository geneCoordinateRepository
        )
        {
            _context = context;
            _mapper = mapper;
            _geneRepository = geneRepository;
        }

        [HttpGet("count")]
        public int GetGeneCount()
        {
            var geneEntities = BuildGeneSearch();
            return geneEntities.Count();

        }

        // GET: api/Genes
        [HttpGet]
        public IEnumerable<GeneDto> GetGenes()
        {
            var geneEntities = BuildGeneSearch();

            var geneQuerable = geneEntities
                .Include(gene => gene.Symbol)
                .Include(gene => gene.GeneLocations)
                .ThenInclude(geneLocation => geneLocation.Chromosome)
                .Include(gene => gene.GeneLocations)
                .ThenInclude(geneLocation => geneLocation.GeneCoordinates)
                .Include(gene => gene.GeneName)
                .Include(gene => gene.Synonym)
                .AsQueryable()
                ;

            var query = HttpContext.Request.Query;
            if (query.ContainsKey(QPageStart) && query.ContainsKey(QPageCount))
            {
                try
                {
                    var skip = Convert.ToInt32(query[QPageStart]);
                    var take = Convert.ToInt32(query[QPageCount]);
                    geneQuerable = geneQuerable
                        .Skip(skip)
                        .Take(take);
                }
                catch (FormatException)
                {
                }
            }
            var genes = geneQuerable.ToList();
            if (genes.Count == 1 && genes[0] == null) genes.Clear();

            var geneDtos = _mapper.Map<IList<GeneDto>>(genes);

            return geneDtos;
        }

        private IQueryable<Gene> BuildGeneSearch()
        {
            var query = HttpContext.Request.Query;
            IQueryable<Gene> geneQuerable = _context.Gene;
            if (query.Count == 0) return geneQuerable;
            var predicate = PredicateBuilder.False<Gene>();
            if (query.ContainsKey(QGlobalFilter))
            {
                /*
                predicate = predicate
                    .Or(
                        g => g.Symbol.Contains(_context.Symbol.Single(s => s.Name == "A1BG"))
                        );
                        */
                geneQuerable = (
                    from g in _context.Gene
                    join s in _context.Symbol on g.Id equals s.GeneId
//                    join gn in _context.GeneName on g.Id equals gn.GeneId
                    where
                        EF.Functions.Like(s.Name, "%" + query[QGlobalFilter] + "%")
//                        || EF.Functions.Like(gn.Name, "%" + query[_qGlobalFilter] + "%")
                    select g
                );
            }

            //return geneQuerable.Where(predicate);
            return geneQuerable;
        }

        // GET: api/Genes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGene([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gene = await _geneRepository.All()
                .Include(g => g.GeneName)
                .Include(g => g.GeneLocations)
                .Include(g => g.GeneOriginType)
                .Include(g => g.AnnotationGene)
                .ThenInclude(annotationGene => annotationGene.Annotation)
                .ThenInclude(annotation => annotation.AppUser)
                .Include(g => g.Symbol)
                .Include(g => g.Synonym)
                .Include(g => g.GeneLocations)
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

            return CreatedAtAction("GetGene", new {id = gene.Id}, gene);
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