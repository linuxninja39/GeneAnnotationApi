using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GeneAnnotationApi.Controllers
{
    [Route("api/[controller]")]
    public class AnnotationsController : Controller
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IMapper _mapper;

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

        // POST api/values
        [HttpPost("gene/{geneId}")]
        public async Task<IActionResult> SaveGeneAnnotation(int geneId, [FromBody] AnnotationDto annotationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (annotationDto.AppUserId.Equals(0))
            {
                if (annotationDto.AppUser == null || annotationDto.AppUser.Id.Equals(0))
                {
                    return BadRequest("AppUserId required");
                }
                annotationDto.AppUserId = annotationDto.AppUser.Id;
            }
            if (annotationDto.Id > 0)
            {
                return BadRequest("Only NEW annotation are valid");
            }

            annotationDto.AppUser = null;

            var annotationEntity = _mapper.Map<Annotation>(annotationDto);
            _context.Annotation.Add(annotationEntity);
            _context.SaveChanges();

            var annotationGeneEntity = new AnnotationGene { GeneId = geneId, AnnotationId = annotationEntity.Id};

            _context.AnnotationGene.Add(annotationGeneEntity);
            _context.SaveChanges();
            
            return Ok(_mapper.Map<AnnotationDto>(annotationEntity));
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