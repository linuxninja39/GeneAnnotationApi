using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Controllers
{
    [Route("api/[controller]")]
    public class GeneVariantsController : Controller
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IMapper _mapper;

        public GeneVariantsController(
            GeneAnnotationDBContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geneVariant = await _context
                .GeneVariant
                .Include(gv => gv.ZygosityType)
                .Include(gv => gv.VariantType)
                .Include(gv => gv.CallType)
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
                .SingleOrDefaultAsync(m => m.Id == id);

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

            geneVariantEntity = await _context
                .GeneVariant
                .Include(gv => gv.ZygosityType)
                .Include(gv => gv.CallType)
                .Include(gv => gv.VariantType)
                .SingleOrDefaultAsync(m => m.Id == geneVariantEntity.Id);

            var retDto = _mapper.Map<GeneVariantDto>(
                geneVariantEntity
            );

            return Ok(retDto);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("{geneVariantId}/Literature")]
        public async Task<IActionResult> GetGeneVariantLiterature(
            int literatureId,
            int geneVariantId
        )
        {
            try
            {
                var geneVariantLiteratures = _context.GeneVariantLiterature
                    .Include(gvl => gvl.AnnotationGeneVariantLiterature)
                        .ThenInclude(gvl => gvl.Annotation)
                            .ThenInclude(a => a.AppUser)
                    .Include(gvl => gvl.Literature)
                        .ThenInclude(l => l.AuthorLiterature)
                            .ThenInclude(al => al.Author)
                    .Where(gvl => gvl.GeneVariantId == geneVariantId)
                    .ToList()
                    ;
                ;

                return Ok(_mapper.Map<GeneVariantLiteratureDto[]>(geneVariantLiteratures));
            }
            catch (InvalidOperationException e)
            {
                return NotFound();
            }
        }
    }
}