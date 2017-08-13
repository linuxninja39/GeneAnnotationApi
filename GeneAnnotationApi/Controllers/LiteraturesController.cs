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

        [HttpGet]
        public async Task<IActionResult> GetLiteratures()
        {
            if (_context.Literature == null) return BadRequest();
            var literatureEntities = _context.Literature;
            var literatureDtos = literatureEntities.ProjectTo<LiteratureDto>().ToList();
            return Ok(literatureDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddLiterature([FromBody] LiteratureDto literatureDto)
        {
            return Ok(literatureDto);
        }

        [HttpPost("GeneVariant/{geneVariantId}")]
        public async Task<IActionResult> AddGeneVariantLiterature(int geneVariantId, [FromBody] LiteratureDto literatureDto)
        {
            return Ok(literatureDto);
        }
    }
}