using System;
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

        [HttpGet]
        public ObjectResult GetLiteratures()
        {
            if (_context.Literature == null) return new ObjectResult(BadRequest());
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
        public ObjectResult AddLiterature([FromBody] LiteratureDto literatureDto)
        {
            return Ok(literatureDto);
        }

        [HttpGet("{literatureId}/GeneVariant/{geneVariantId}")]
        public ObjectResult GetGeneVariantLiterature(
            int literatureId,
            int geneVariantId
        )
        {
            try
            {
                var geneVariantLiterature = _context.GeneVariantLiterature
                    .Include(gvl => gvl.AnnotationGeneVariantLiterature)
                    .ThenInclude(gvl => gvl.Annotation)
                    .ThenInclude(a => a.AppUser)
                    .Include(gvl => gvl.Literature)
                    .ThenInclude(l => l.AuthorLiterature)
                    .ThenInclude(al => al.Author)
                    .Single(gvl => gvl.LiteratureId == literatureId && gvl.GeneVariantId == geneVariantId);
                ;

                return Ok(_mapper.Map<GeneVariantLiteratureDto>(geneVariantLiterature));
            }
            catch (InvalidOperationException)
            {
                return new ObjectResult(NotFound());
            }
        }

        [HttpPost("{literatureId}/GeneVariant/{geneVariantId}")]
        public ObjectResult AddGeneVariantLiterature(
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
