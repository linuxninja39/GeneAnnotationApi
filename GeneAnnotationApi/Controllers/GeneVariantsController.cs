using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Controllers
{
    [Route("api/[controller]")]
    public class GeneVariantsController : Controller
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IGeneVariantRepository _geneVariantRepository;

        private const string QVariantStart = "start";
        private const string QVariantEnd = "end";
        private const string QPageStart = "pageStart";
        private const string QPageCount = "pageCount";

        public GeneVariantsController(
            GeneAnnotationDBContext context,
            IGeneVariantRepository geneVariantRepository,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
            _geneVariantRepository = geneVariantRepository;
        }

        [HttpGet]
        public IEnumerable<GeneVariantDto> GetByRange()
        {
            var query = HttpContext.Request.Query;
            IQueryable<GeneVariant> geneVariantQueryable = _context.GeneVariant;
            if (query.ContainsKey(QVariantStart) && query.ContainsKey(QVariantStart))
            {
                if (int.TryParse(query[QVariantStart], out var start) &&
                    int.TryParse(query[QVariantEnd], out var end))
                {
                    geneVariantQueryable = _geneVariantRepository.FindByRange(start, end);
                }
            }
            
            if (query.ContainsKey(QPageCount) && query.ContainsKey(QPageStart))
            {
                if (int.TryParse(query[QPageCount], out var take) &&
                    int.TryParse(query[QPageStart], out var skip))
                {
                    geneVariantQueryable = geneVariantQueryable.Skip(skip).Take(take);
                }
            }

            geneVariantQueryable = geneVariantQueryable
                .Include(gv => gv.CallTypeGeneVariants)
                .ThenInclude(ctgv => ctgv.CallType)
                .Include(gv => gv.VariantType)
                .Include(gv => gv.ZygosityType)
                ;

            var geneVariants = geneVariantQueryable.ToList();

            var geneVariantDtos = _mapper.Map<IList<GeneVariantDto>>(geneVariants);

            return geneVariantDtos;
        }

        // GET api/genevariants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geneVariant = await _context
                .GeneVariant
                .Include(gv => gv.ZygosityType)
                .Include(gv => gv.VariantType)
                .Include(gv => gv.CallTypeGeneVariants)
                .ThenInclude(geneVariantCallType => geneVariantCallType.CallType)
                .Include(gv => gv.AnnotationGeneVariant)
                .ThenInclude(agv => agv.Annotation)
                .ThenInclude(a => a.AppUser)
                .Include(gv => gv.GeneVariantLiterature)
                .ThenInclude(gvl => gvl.Literature)
                .ThenInclude(l => l.AuthorLiterature)
                .ThenInclude(al => al.Author)
                .Include(gv => gv.GeneVariantLiterature)
                .ThenInclude(gvl => gvl.AnnotationGeneVariantLiterature)
                .ThenInclude(agvl => agvl.Annotation)
                .SingleOrDefaultAsync(gv => gv.Id == id);

            if (geneVariant == null)
            {
                return NotFound();
            }

            var geneVariantDto = _mapper.Map<GeneVariantDto>(geneVariant);

            return Ok(geneVariantDto);
        }

        // POST api/GeneVariants
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeneVariantDto geneVariantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geneVariantEntity = _mapper.Map<GeneVariant>(geneVariantDto);
            _context.GeneVariant.Add(geneVariantEntity);
            _context.SaveChanges();

            var entity = geneVariantEntity;
            geneVariantEntity = await _context
                .GeneVariant
                .Include(gv => gv.ZygosityType)
                .Include(gv => gv.CallTypeGeneVariants)
                .ThenInclude(geneVariantCallType => geneVariantCallType.CallType)
                .Include(gv => gv.VariantType)
                .SingleOrDefaultAsync(m => m.Id == entity.Id);

            var retDto = _mapper.Map<GeneVariantDto>(
                geneVariantEntity
            );

            return Ok(retDto);
        }

        [HttpGet("{geneVariantId}/Literature")]
        public OkObjectResult GetGeneVariantLiterature(
            int literatureId,
            int geneVariantId
        )
        {
            try
            {
                var geneVariantLiteratures = _context.GeneVariantLiterature
                        .Where(gvl => gvl.GeneVariantId == geneVariantId)
                        .Include(gvl => gvl.AnnotationGeneVariantLiterature)
                        .ThenInclude(gvl => gvl.Annotation)
                        .ThenInclude(a => a.AppUser)
                        .Include(gvl => gvl.Literature)
                        .ThenInclude(l => l.AuthorLiterature)
                        .ThenInclude(al => al.Author)
                        .ToList()
                    ;

                return Ok(_mapper.Map<GeneVariantLiteratureDto[]>(geneVariantLiteratures));
            }
            catch (InvalidOperationException)
            {
                return new OkObjectResult(NotFound());
            }
        }
    }
}