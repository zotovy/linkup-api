using System;
using System.Linq;
using API.DTO;
using API.DTO.Link;
using API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Link;

namespace API.Controllers {

    [ApiController]
    [Route("api/{v:apiVersion}/link")]
    public class LinkController : ControllerBase {

        private readonly ILinkService _service;

        public LinkController(ILinkService service) {
            _service = service;
        }

        [HttpPost]
        [Authorize, ValidationErrorFilter]
        public IActionResult CreateLink([FromBody] CreateLinkRequestDto body) {
            var validity = body.Validate();
            if (validity != null) return BadRequest(validity);

            // Authorize user
            long tokenId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (body.userId != tokenId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };
            
            var link = body.ToDomain();
            var id = _service.Add(link);

            return Ok(new CreateLinkSuccessResponseDto(id));
        }

        [HttpPut("{id}")]
        [Authorize, ValidationErrorFilter]
        public IActionResult UpdateLink(int id, [FromBody] UpdateLinkRequestDto body) {
            var validity = body.Validate();
            if (validity != null) return BadRequest(validity);
            
            // Authorize user
            var authorId = _service.GetAuthorIdOf(id);
            long tokenId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (authorId != tokenId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };
        
            var link = body.ToDomain();
            link.Id = id;
            
            _service.ChangeTo(link);

            return Ok(new EmptyOkDto());
        }

        [HttpDelete("{id}")]
        [Authorize, ValidationErrorFilter]
        public IActionResult RemoveLink(int id) {
            // Authorize user
            var authorId = _service.GetAuthorIdOf(id);
            long tokenId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (authorId != tokenId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };
            
            _service.Remove(id);

            return Ok(new EmptyOkDto());
        }
    }
}