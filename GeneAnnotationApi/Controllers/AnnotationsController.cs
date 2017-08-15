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
    public class AnnotationsController : Controller
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IMapper _mapper;
        private ObjectResult _invalidModelStateMessage;

        public AnnotationsController(
            GeneAnnotationDBContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("Gene/{geneId}")]
        public async Task<IActionResult> SaveGeneAnnotation(int geneId, [FromBody] AnnotationDto annotationDto)
        {
            if (!isModelValid(annotationDto))
            {
                return _invalidModelStateMessage;
            }

            if (annotationDto.AppUserId.Equals(0))
            {
                annotationDto.AppUserId = annotationDto.AppUser.Id;
            }
            annotationDto.AppUser = null;

            var annotationEntity = _mapper.Map<Annotation>(annotationDto);
            _context.Annotation.Add(annotationEntity);
            _context.SaveChanges();

            var annotationGeneEntity = new AnnotationGene {GeneId = geneId, AnnotationId = annotationEntity.Id};

            _context.AnnotationGene.Add(annotationGeneEntity);
            _context.SaveChanges();

            annotationEntity = _context.Annotation
                .Include(annotation => annotation.AppUser)
                .Single(annotation => annotation.Id == annotationEntity.Id);

            return Ok(_mapper.Map<AnnotationDto>(annotationEntity));
        }

        [HttpPost("GeneVariant/{geneVariantId}")]
        public async Task<IActionResult> SaveGeneVariantAnnotation(
            int geneVariantId,
            [FromBody] AnnotationDto annotationDto
        )
        {
            if (!isModelValid(annotationDto))
            {
                return _invalidModelStateMessage;
            }

            if (annotationDto.AppUserId.Equals(0))
            {
                annotationDto.AppUserId = annotationDto.AppUser.Id;
            }
            annotationDto.AppUser = null;

            var annotationEntity = _mapper.Map<Annotation>(annotationDto);
            _context.Annotation.Add(annotationEntity);
            _context.SaveChanges();

            var annotationGeneVariantEntity = new AnnotationGeneVariant
            {
                GeneVariantId = geneVariantId,
                AnnotationId = annotationEntity.Id
            };

            _context.AnnotationGeneVariant.Add(annotationGeneVariantEntity);
            _context.SaveChanges();

            annotationEntity = _context.Annotation
                .Include(annotation => annotation.AppUser)
                .Single(annotation => annotation.Id == annotationEntity.Id);

            return Ok(_mapper.Map<AnnotationDto>(annotationEntity));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost("GeneVariantLiterature/{geneVariantLiteratureId}")]
        public async Task<IActionResult> AddAnnotationGeneVariantLiterature(
            int geneVariantLiteratureId,
            [FromBody] AnnotationDto annotationDto
        )
        {
            try
            {
                var geneVariantLiterature = _context.GeneVariantLiterature
                    .Find(geneVariantLiteratureId);
                var annotationEntity = _mapper.Map<Annotation>(annotationDto);
                _context.Annotation.Add(annotationEntity);
                _context.SaveChanges();
                var annotationGeneVariantLiterature = new AnnotationGeneVariantLiterature
                {
                    GeneVariantLiteratureId = geneVariantLiterature.Id,
                    AnnotationId = annotationEntity.Id
                };
                _context.AnnotationGeneVariantLiterature.Add(annotationGeneVariantLiterature);
                _context.SaveChanges();


                annotationEntity = _context.Annotation
                    .Include(annotation => annotation.AppUser)
                    .Single(annotation => annotation.Id == annotationEntity.Id);

                return Ok(_mapper.Map<AnnotationDto>(annotationEntity));
            }
            catch (InvalidOperationException e)
            {
                return NotFound("could not find GeneVariantLiterature");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool isModelValid(AnnotationDto annotationDto)
        {
            if (!ModelState.IsValid)
            {
                _invalidModelStateMessage = BadRequest(ModelState);
                return false;
            }

            if (annotationDto.AppUserId.Equals(0))
            {
                if (annotationDto.AppUser == null || annotationDto.AppUser.Id.Equals(0))
                {
                    _invalidModelStateMessage = BadRequest("AppUserId required");
                    return false;
                }
                annotationDto.AppUserId = annotationDto.AppUser.Id;
            }
            if (annotationDto.Id > 0)
            {
                _invalidModelStateMessage = BadRequest("Only NEW annotation are valid");
                return false;
            }


            return true;
        }
    }
}