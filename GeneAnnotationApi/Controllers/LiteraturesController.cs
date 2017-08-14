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
        private ObjectResult _invalidModelStateMessage;

        public LiteraturesController(GeneAnnotationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLiteratures()
        {
            if (_context.Literature == null) return BadRequest();
            var literatureEntities = _context.Literature
                    .Include(literature => literature.AuthorLiterature)
                    .ThenInclude(authorLiterature => authorLiterature.Author)
                    .Include(literature => literature.AnnotationLiterature)
                    .ThenInclude(annotationLiterature => annotationLiterature.Annotation)
                ;
            var literatureDtos = literatureEntities.ProjectTo<LiteratureDto>().ToList();
            return Ok(literatureDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddLiterature([FromBody] LiteratureDto literatureDto)
        {
            return Ok(literatureDto);
        }

        [HttpGet("{literatureId}/GeneVariant/{geneVariantId}")]
        public async Task<IActionResult> GetGeneVariantLiterature(
            int literatureId,
            int geneVariantId
        )
        {
            var geneVariantLiteratures = _context.GeneVariantLiterature
                .Where(gvl => gvl.LiteratureId == literatureId && gvl.GeneVariantId == geneVariantId);

            return Ok(geneVariantLiteratures);
        }

        [HttpPost("{literatureId}/GeneVariant/{geneVariantId}")]
        public async Task<IActionResult> AddGeneVariantLiterature(
            int literatureId,
            int geneVariantId
        )
        {
            var geneVariantLiterature = new GeneVariantLiterature
            {
                GeneVariantId = geneVariantId,
                LiteratureId = literatureId
            };

            _context.GeneVariantLiterature.Add(geneVariantLiterature);
            _context.SaveChanges();

            geneVariantLiterature = _context.GeneVariantLiterature
                .Include(gvl => gvl.Literature)
                    .ThenInclude(l => l.AnnotationLiterature)
                        .ThenInclude(al => al.Annotation)
                .Include(gvl => gvl.Literature)
                    .ThenInclude(l => l.AuthorLiterature)
                        .ThenInclude(al => al.Author)
                .Include(gvl => gvl.AnnotationGeneVariantLiterature)
                    .ThenInclude(agvl => agvl.Annotation)
                .Single(gvl => gvl.Id == geneVariantLiterature.Id);
            
            return Ok(_mapper.Map<GeneVariantLiteratureDto>(geneVariantLiterature));
        }
    }
}