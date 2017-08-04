using System.Collections.Generic;
using System.Threading.Tasks;
using GeneAnnotationApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GeneAnnotationApi.Controllers
{
    [Route("api/[controller]")]
    public class GeneVariantsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/GeneVariants
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GeneVariantDto geneVariantDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (geneVariantDto.Id == null)
            {
                return BadRequest();
            }
            return Ok(geneVariantDto);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
