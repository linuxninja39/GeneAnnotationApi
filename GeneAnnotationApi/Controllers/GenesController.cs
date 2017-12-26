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

        private static readonly string _qStart = "start";
        private static readonly string _qEnd = "end";
        private static readonly string _qGlobalFilter = "globalFilter";

        public GenesController(GeneAnnotationDBContext context, IMapper mapper, IGeneRepository geneRepository,
            IGeneCoordinateRepository geneCoordinateRepository
        )
        {
            _context = context;
            _mapper = mapper;
            _geneRepository = geneRepository;
        }

        // GET: api/Genes
        [HttpGet]
        public IEnumerable<GeneDto> GetGenes()
        {
            var geneEntities = BuildGeneSearch();

            var genes = geneEntities
                .Include(gene => gene.Symbol)
                .Include(gene => gene.GeneLocations)
                .ThenInclude(geneLocation => geneLocation.Chromosome)
                .Include(gene => gene.GeneLocations)
                .ThenInclude(geneLocation => geneLocation.GeneCoordinates)
                .Include(gene => gene.GeneName)
                .Include(gene => gene.Synonym)
                .ToList();
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
            if (query.ContainsKey(_qGlobalFilter))
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
                    where
                        EF.Functions.Like(s.Name, "%" + query[_qGlobalFilter] + "%")
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