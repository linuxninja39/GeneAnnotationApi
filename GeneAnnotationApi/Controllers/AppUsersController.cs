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
    public class AppUsersController : Controller
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IMapper _mapper;

        public AppUsersController(GeneAnnotationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppUsers()
        {
            var appUserEntities = _context.AppUser;
            var appUserDtos = appUserEntities.ProjectTo<AppUserDto>().ToList();
            return Ok(appUserDtos);
        }

        [HttpPost]
        public async Task<IActionResult> GetOrAddAppUser([FromBody] AppUserDto appUserDto)
        {
            if (appUserDto.Id > 0)
            {
                return Ok(
                    _mapper.Map<AppUserDto>(
                        _context.AppUser.Find(appUserDto.Id))
                );
            }
            if (appUserDto.Name != null)
            {
                var appUser = await _context.AppUser
                    .SingleOrDefaultAsync(m => m.Name == appUserDto.Name);
                if (appUser == null)
                {
                    appUser = new AppUser{Name = appUserDto.Name};
                    _context.AppUser.Add(appUser);
                    _context.SaveChanges();
                }
                return Ok(
                    _mapper.Map<AppUserDto>(
                        appUser
                        )
                    );
            }
            return BadRequest("user not found");
        }

        [HttpPost("GeneVariant/{geneVariantId}/literatureId")]
        public async Task<IActionResult> AddGeneVariantLiterature(
            int geneVariantId,
            int literatureId
        )
        {
            var geneVariantLiterature = new GeneVariantLiterature
            {
                GeneVariantId = geneVariantId,
                LiteratureId = literatureId
            };

            _context.GeneVariantLiterature.Add(geneVariantLiterature);
            var literatureEntity = _context.Literature
                .SingleOrDefaultAsync(m => m.Id == literatureId);

            return Ok(_mapper.Map<LiteratureDto>(literatureEntity));
        }
    }
}