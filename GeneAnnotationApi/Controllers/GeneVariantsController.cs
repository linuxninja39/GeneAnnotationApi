using System.Collections.Generic;
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
    }
}